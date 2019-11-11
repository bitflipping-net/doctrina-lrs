using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Models;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Queries
{
    public class PagedStatementsQueryHandler : IRequestHandler<PagedStatementsQuery, PagedStatementsResult>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _cache;

        public PagedStatementsQueryHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        public async Task<PagedStatementsResult> Handle(PagedStatementsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Statements.AsNoTracking();

            //if(request.VoidedStatementId.HasValue)
            //{
            //    query = query.Where(x => x.StatementId == request.VoidedStatementId.Value && x.Voided);
            //}

            //if (request.StatementId.HasValue)
            //{
            //    query = query.Where(x => x.StatementId == request.StatementId.Value && !x.Voided);
            //}

            if (request.VerbId != null)
            {
                string verbHash = request.VerbId.ComputeHash();
                query = query.Where(x => x.Verb.Hash == verbHash);
            }

            if (request.Agent != null)
            {
                var actor = _mapper.Map<AgentEntity>(request.Agent);
                var currentAgent = await _context.Agents.AsNoTracking().FirstOrDefaultAsync(x => x.ObjectType == actor.ObjectType 
                    && x.Hash == actor.Hash, cancellationToken);
                if (currentAgent != null)
                {
                    Guid agentId = currentAgent.AgentId;
                    if (request.RelatedAgents.GetValueOrDefault())
                    {
                        query = (
                            from statement in query
                            where statement.Actor.AgentId == agentId
                            || (
                                statement.Object.ObjectType == EntityObjectType.Agent &&
                                statement.Object.Agent.AgentId == agentId
                            ) || (
                                statement.Object.ObjectType == EntityObjectType.SubStatement &&
                                (
                                    statement.Object.SubStatement.Actor.AgentId == agentId ||
                                    statement.Object.SubStatement.Object.ObjectType == EntityObjectType.Agent &&
                                    statement.Object.SubStatement.Object.Agent.AgentId == agentId
                                )
                            )
                            select statement);
                    }
                    else
                    {
                        query = query.Where(x => x.Actor.Hash == actor.Hash);
                    }
                }
                else
                {
                    return new PagedStatementsResult();
                }
            }

            if (request.ActivityId != null)
            {
                string activityHash = request.ActivityId.ComputeHash();

                if (request.RelatedActivities.GetValueOrDefault())
                {
                    query = (
                        from statement in query
                        where (
                            statement.Object.ObjectType == EntityObjectType.SubStatement && (
                                statement.Object.SubStatement.Object.ObjectType == EntityObjectType.Activity &&
                                statement.Object.SubStatement.Object.Activity.Hash == activityHash
                            ) ||
                            (
                                statement.Context != null && statement.Context.ContextActivities != null &&
                                (
                                    statement.Context.ContextActivities.Category.Any(x=> x.Hash == activityHash) ||
                                    statement.Context.ContextActivities.Parent.Any(x => x.Hash == activityHash) ||
                                    statement.Context.ContextActivities.Grouping.Any(x => x.Hash == activityHash) ||
                                    statement.Context.ContextActivities.Other.Any(x => x.Hash == activityHash)
                                )
                            ) ||
                            (
                                statement.Object.ObjectType == EntityObjectType.Activity &&
                                statement.Object.Activity.Hash == activityHash
                            )
                        )
                        select statement
                    );
                }
                else
                {
                    query = query.Where(x => x.Object.ObjectType == EntityObjectType.Activity && x.Object.Activity.Hash == activityHash);
                }
            }

            if (request.Registration.HasValue)
            {
                Guid registration = request.Registration.Value;
                query = (
                    from statement in query
                    where statement.Context != null && statement.Context.Registration == registration
                    select statement
                );
            }

            if (request.Ascending.GetValueOrDefault())
            {
                query = query.OrderBy(x => x.Timestamp);
            }
            else
            {
                query = query.OrderByDescending(x => x.Timestamp);
            }

            int skipRows = 0;
            //if (!string.IsNullOrEmpty(request.MoreToken))
            //{
            //    if(_cache.TryGetValue(request.MoreToken, out int skipRows))
            //    {
            //        skipRows += request.Limit.Value;
            //    }
            //}

            

            int pageSize = request.Limit ?? 1000;

            IQueryable<PagedQuery> pagedQuery = null;

            if (request.Attachments.GetValueOrDefault())
            {
                pagedQuery = query.Select(p => new PagedQuery { 
                    Statement = new StatementEntity
                    {
                        StatementId = p.StatementId,
                        FullStatement = p.FullStatement
                    },
                    TotalCount = query.LongCount()
                });
            }
            else
            {
                pagedQuery = query.Select(p => new PagedQuery
                { 
                    Statement = new StatementEntity
                    {
                        StatementId = p.StatementId,
                        FullStatement = p.FullStatement,
                        Attachments = p.Attachments,
                    },
                    TotalCount = query.LongCount()
                });
            }

            var result = await pagedQuery.Skip(skipRows).Take(pageSize)
                .ToListAsync(cancellationToken);

            if (result == null)
            {
                return new PagedStatementsResult();
            }

            long totalCount = result.FirstOrDefault()?.TotalCount ?? 0;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            List<Statement> statements = pagedQuery.Select(p => _mapper.Map<Statement>(p.Statement)).ToList();

            var statementCollection = new StatementCollection(statements);

            string moreToken = totalPages > 1 ? Guid.NewGuid().ToString() : null;
            // TODO: Save query

            return new PagedStatementsResult(statementCollection, moreToken);
        }
    }
}
