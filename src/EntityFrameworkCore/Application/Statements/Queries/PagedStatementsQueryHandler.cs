using AutoMapper;
using Doctrina.Application.Statements.Models;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly DoctrinaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public PagedStatementsQueryHandler(DoctrinaDbContext context, IMapper mapper, IDistributedCache distributedCache)
        {
            _context = context;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<PagedStatementsResult> Handle(PagedStatementsQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.MoreToken))
            {
                string token = request.MoreToken;
                string jsonString = await _distributedCache.GetStringAsync(token, cancellationToken);
                request = PagedStatementsQuery.FromJson(jsonString);
                request.MoreToken = null;
            }

            var sb = new StringBuilder();

            sb.AppendLine("WHERE FullStatement IS NOT NULL");
            //sb.Append($"WHERE TenantId = '{TenantId}'");

            var query = _context.Statements.AsNoTracking();

            if (request.VerbId != null)
            {
                string verbHash = request.VerbId.ComputeHash();
                sb.AppendLine("AND JSON_VALUE(FullStatement, '$.verb.id') = @VerId");
            }

            if (request.Agent != null)
            {
                sb.AppendLine($"AND JSON_VALUE(FullStatement, '$.actor.objectType') = '{request.Agent.ObjectType}'");

                if(request.Agent.Account != null)
                {
                    sb.AppendLine($"AND WHERE JSON_VALUE(FullStatement, '$.actor.account.name') = '{request.Agent.Account.Name}'");
                    sb.AppendLine($"AND WHERE JSON_VALUE(FullStatement, '$.actor.account.homePage') = '{request.Agent.Account.HomePage}'");
                }
                if(request.Agent.Mbox != null)
                {

                }

                var actor = _mapper.Map<AgentEntity>(request.Agent);
                var currentAgent = await _context.Agents.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ObjectType == actor.ObjectType
                    && x.AgentId == actor.Id, cancellationToken);
                if (currentAgent != null)
                {
                    Guid agentId = currentAgent.AgentId;
                    if (request.RelatedAgents.GetValueOrDefault())
                    {
                        sb.AppendLine($"OR (");
                        sb.AppendLine($"JSON_VALUE(FullStatement, '$.object.actor.mbox')");
                        query = (
                            from statement in query
                            let objectT = statement.Object.ObjectType
                            where statement.Actor.AgentId == agentId
                            || (
                                objectT == ObjectType.Agent &&
                                statement.Object.Agent.AgentId == agentId
                            ) || (
                                objectT == ObjectType.SubStatement &&
                                (
                                    statement.Object.As<SubStatementEntity>().Actor.AgentId == agentId ||
                                    statement.Object.As<SubStatementEntity>().Object.ObjectType == ObjectType.Agent &&
                                    statement.Object.As<SubStatementEntity>().Object.Agent.AgentId == agentId
                                )
                            )
                            select statement);
                        sb.AppendLine($")");
                    }
                    else
                    {
                        query = query.Where(x => x.Actor.AgentId == actor.Id);
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
                            statement.Object.ObjectType == ObjectType.SubStatement && (
                                statement.Object.SubStatement.Object.ObjectType == ObjectType.Activity &&
                                statement.Object.SubStatement.Object.Activity.Hash == activityHash
                            ) ||
                            (
                                statement.Context != null && statement.Context.ContextActivities != null &&
                                (
                                    statement.Context.ContextActivities.Category.Any(x => x.Hash == activityHash) ||
                                    statement.Context.ContextActivities.Parent.Any(x => x.Hash == activityHash) ||
                                    statement.Context.ContextActivities.Grouping.Any(x => x.Hash == activityHash) ||
                                    statement.Context.ContextActivities.Other.Any(x => x.Hash == activityHash)
                                )
                            ) ||
                            (
                                statement.Object.ObjectType == ObjectType.Activity &&
                                statement.Object.Activity.Hash == activityHash
                            )
                        )
                        select statement
                    );
                }
                else
                {
                    query = query.Where(x => x.Object == ObjectType.Activity && x.Object.Activity.Hash == activityHash);
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
                query = query.OrderBy(x => x.CreatedAt);
            }
            else
            {
                query = query.OrderByDescending(x => x.CreatedAt);
            }

            if (request.Since.HasValue)
            {
                query = query.Where(x => x.CreatedAt > request.Since.Value);
            }

            if (request.Until.HasValue)
            {
                query = query.Where(x => x.CreatedAt < request.Until.Value);
            }

            int pageSize = request.Limit ?? 1000;
            int skipRows = request.PageIndex * pageSize;

            IQueryable<StatementEntity> pagedQuery = null;

            _context.Statements.FromSqlInterpolated($"PagedStatementsQuery {request.Ascending} {request.Limit}");

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
                request.MoreToken = Guid.NewGuid().ToString();
                request.PageIndex += 1;
                if (!request.Until.HasValue)
                {
                    request.Until = DateTimeOffset.UtcNow;
                }
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60 * 10));
                await _distributedCache.SetStringAsync(request.MoreToken, request.ToJson(), options, cancellationToken);

                return new PagedStatementsResult(statements, request.MoreToken);
            }

            return new PagedStatementsResult(statements);
        }
    }
}
