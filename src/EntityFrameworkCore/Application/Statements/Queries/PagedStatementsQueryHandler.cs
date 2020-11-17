using AutoMapper;
using Doctrina.Application.Activities.Queries;
using Doctrina.Application.Agents.Queries;
using Doctrina.Application.Statements.Models;
using Doctrina.Domain.Entities;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IDistributedCache _distributedCache;

        public PagedStatementsQueryHandler(IDoctrinaDbContext context, IMediator meditor, IMapper mapper, IDistributedCache distributedCache)
        {
            _context = context;
            _mapper = mapper;
            _mediator = meditor;
            _distributedCache = distributedCache;
        }

        public async Task<PagedStatementsResult> Handle(PagedStatementsQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.Cursor))
            {
                string token = request.Cursor;
                string jsonString = await _distributedCache.GetStringAsync(token, cancellationToken);
                request = PagedStatementsQuery.FromJson(jsonString);
                request.Cursor = null;
            }

            var query = _context.Statements.AsNoTracking();

            if (request.VerbId != null)
            {
                string verbHash = request.VerbId.ComputeHash();
                query = query.Where(x => x.Verb.Hash == verbHash);
            }

            if (request.Agent != null)
            {
                var currentAgent = await _mediator.Send(GetAgentQuery.Create(request.Agent));

                if (currentAgent != null)
                {
                    Guid agentId = currentAgent.AgentId;
                    if (request.RelatedAgents.GetValueOrDefault())
                    {
                        query = (
                            from sta in query
                            join rel in _context.ObjectRelations
                                on sta.StatementId equals rel.ParentId
                            where sta.Actor.AgentId == agentId
                            || (
                                rel.ChildObjectType == EntityObjectType.Agent &&
                                rel.ChildId == agentId
                            )
                            select sta
                        );
                    }
                    else
                    {
                        query = query.Where(x => x.ActorId == currentAgent.AgentId);
                    }
                }
                else
                {
                    return new PagedStatementsResult();
                }
            }

            if (request.ActivityId != null)
            {
                var activity = await _mediator.Send(GetActivityQuery.Create(request.ActivityId), cancellationToken);
                if (activity == null)
                    return new PagedStatementsResult();

                if (request.RelatedActivities.GetValueOrDefault())
                {
                    query = (
                        from statement in query
                        join subStatement in _context.SubStatements 
                            on new { ObjectId = statement.ObjectId, ObjectType = statement.ObjectType }
                            equals new { ObjectId = subStatement.ObjectId, ObjectType = EntityObjectType.SubStatement }
                        where
                            (
                                statement.ObjectType == EntityObjectType.Activity &&
                                statement.ObjectId == activity.ActivityId
                            ) || (
                                subStatement.ObjectType == EntityObjectType.Activity &&
                                subStatement.ObjectId == activity.ActivityId
                            ) || (
                                statement.Context != null && statement.Context.ContextActivities != null &&
                                (
                                    statement.Context.ContextActivities.Any(x => x.ActivityId == activity.ActivityId)
                                )
                            )
                        select statement
                    );
                }
                else
                {
                    query = query.Where(x => x.ObjectType == EntityObjectType.Activity && x.ObjectId == activity.ActivityId);
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
                query = query.OrderBy(x => x.Stored);
            }
            else
            {
                query = query.OrderByDescending(x => x.Stored);
            }

            if (request.Since.HasValue)
            {
                query = query.Where(x => x.Stored > request.Since.Value);
            }

            if (request.Until.HasValue)
            {
                query = query.Where(x => x.Stored < request.Until.Value);
            }

            int pageSize = request.Limit ?? 1000;
            int skipRows = request.PageIndex * pageSize;

            IQueryable<StatementEntity> pagedQuery = null;

            // Include voiding statements
            query = query.Select(p => p.VoidingStatementId != null ? p.VoidingStatement : p);

            if (!request.Attachments.GetValueOrDefault())
            {
                pagedQuery = query.Select(p => new StatementEntity
                {
                    StatementId = p.StatementId,
                    FullStatement = p.FullStatement
                });
            }
            else
            {
                pagedQuery = query.Select(p => new StatementEntity
                {
                    StatementId = p.StatementId,
                    FullStatement = p.FullStatement,
                    Attachments = p.Attachments,
                });
            }

            var result = await pagedQuery.Skip(skipRows).Take(pageSize + 1)
                .ToListAsync(cancellationToken);

            if (result == null)
            {
                return new PagedStatementsResult();
            }

            List<StatementEntity> statements = result.Take(pageSize).ToList();

            if (result.Count > pageSize)
            {
                request.Cursor = Guid.NewGuid().ToString();
                request.PageIndex += 1;
                if (!request.Until.HasValue)
                {
                    request.Until = DateTimeOffset.UtcNow;
                }
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60 * 10));
                await _distributedCache.SetStringAsync(request.Cursor, request.ToJson(), options, cancellationToken);

                return new PagedStatementsResult(statements, request.Cursor);
            }

            return new PagedStatementsResult(statements);
        }
    }
}
