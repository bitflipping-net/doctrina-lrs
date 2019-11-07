using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Interfaces;
using Doctrina.Application.Statements.Models;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Extensions;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                var currentAgent = await _context.Agents.WhereAgent(x => x, actor).FirstOrDefaultAsync(cancellationToken);
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
                        query = query.WhereAgent(x => x.Actor, actor);
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
                                    statement.Context.ContextActivities.Category.Contains(activityHash) ||
                                    statement.Context.ContextActivities.Parent.Contains(activityHash) ||
                                    statement.Context.ContextActivities.Grouping.Contains(activityHash) ||
                                    statement.Context.ContextActivities.Other.Contains(activityHash)
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

            if (request.Attachments.GetValueOrDefault())
            {
                query = query.Select(p => new StatementEntity
                {
                    StatementId = p.StatementId,
                    FullStatement = p.FullStatement
                });
            }
            else
            {
                query = query.Select(p => new StatementEntity
                {
                    StatementId = p.StatementId,
                    FullStatement = p.FullStatement,
                    Attachments = p.Attachments
                });
            }

            int pageSize = request.Limit ?? 1000;

            var pagedQuery = await query.Skip(skipRows).Take(pageSize)
                .GroupBy(p => new { TotalCount = query.Count() })
                .FirstOrDefaultAsync(cancellationToken);

            if(pagedQuery == null)
            {
                return new PagedStatementsResult();
            }

            int totalCount = pagedQuery.Key.TotalCount;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            List<Statement> statements = pagedQuery.Select(p => _mapper.Map<Statement>(p)).ToList();

            var statementCollection = new StatementCollection(statements);

            string moreToken = totalPages > 1 ? Guid.NewGuid().ToString() : null;
            // TODO: Save query

            return new PagedStatementsResult(statementCollection, moreToken);
        }
    }
}
