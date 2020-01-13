using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Models;
using Doctrina.ExperienceApi.Data;
using Elasticsearch.Net;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Queries
{
    public class PagedStatementsQueryHandler : IRequestHandler<PagedStatementsQuery, PagedStatementsResult>
    {
        private readonly ISearchManager _searchManager;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public PagedStatementsQueryHandler(ISearchManager searchManager, IMapper mapper, IDistributedCache distributedCache)
        {
            _searchManager = searchManager;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<PagedStatementsResult> Handle(PagedStatementsQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.MoreToken))
            {
                request = await GetRequestFromCache(request, cancellationToken);
            }

            int size = request.Limit ?? 1000;
            int fromIndex = request.PageIndex * size;

            var query = new
            {
                from = fromIndex,
                size = size,
                query = new
                {
                    match_all = new { }
                }
                //query = new { }
            };

            var searchResponse = _searchManager.Client.Search<StringResponse>("statements", PostData.Serializable(query));
            var response = JObject.Parse(searchResponse.Body);
            var hits = response["hits"]["hits"] as JArray;
            var statements = hits.Select(x => new Statement(x["_source"].ToString()));

            //if (request.VerbId != null)
            //{
            //    string verbHash = request.VerbId.ComputeHash();
            //    query = query.Where(x => x.Verb.Hash == verbHash);
            //    query = query && Query<StatementEntity>.Match(x => x.Field(c => c.Verb.Hash == verbHash));
            //}

            //if (request.Agent != null)
            //{
            //    var actor = _mapper.Map<AgentEntity>(request.Agent);
            //    var currentAgent = await _context.Agents.AsNoTracking().FirstOrDefaultAsync(x => x.ObjectType == actor.ObjectType 
            //        && x.Hash == actor.Hash, cancellationToken);
            //    if (currentAgent != null)
            //    {
            //        Guid agentId = currentAgent.AgentId;
            //        if (request.RelatedAgents.GetValueOrDefault())
            //        {
            //            query = (
            //                from statement in query
            //                where statement.Actor.AgentId == agentId
            //                || (
            //                    statement.Object.ObjectType == EntityObjectType.Agent &&
            //                    statement.Object.Agent.AgentId == agentId
            //                ) || (
            //                    statement.Object.ObjectType == EntityObjectType.SubStatement &&
            //                    (
            //                        statement.Object.SubStatement.Actor.AgentId == agentId ||
            //                        statement.Object.SubStatement.Object.ObjectType == EntityObjectType.Agent &&
            //                        statement.Object.SubStatement.Object.Agent.AgentId == agentId
            //                    )
            //                )
            //                select statement);
            //        }
            //        else
            //        {
            //            query = query.Where(x => x.Actor.Hash == actor.Hash);
            //        }
            //    }
            //    else
            //    {
            //        return new PagedStatementsResult();
            //    }
            //}

            //if (request.ActivityId != null)
            //{
            //    string activityHash = request.ActivityId.ComputeHash();

            //    if (request.RelatedActivities.GetValueOrDefault())
            //    {
            //        query = (
            //            from statement in query
            //            where (
            //                statement.Object.ObjectType == EntityObjectType.SubStatement && (
            //                    statement.Object.SubStatement.Object.ObjectType == EntityObjectType.Activity &&
            //                    statement.Object.SubStatement.Object.Activity.Hash == activityHash
            //                ) ||
            //                (
            //                    statement.Context != null && statement.Context.ContextActivities != null &&
            //                    (
            //                        statement.Context.ContextActivities.Category.Any(x=> x.Hash == activityHash) ||
            //                        statement.Context.ContextActivities.Parent.Any(x => x.Hash == activityHash) ||
            //                        statement.Context.ContextActivities.Grouping.Any(x => x.Hash == activityHash) ||
            //                        statement.Context.ContextActivities.Other.Any(x => x.Hash == activityHash)
            //                    )
            //                ) ||
            //                (
            //                    statement.Object.ObjectType == EntityObjectType.Activity &&
            //                    statement.Object.Activity.Hash == activityHash
            //                )
            //            )
            //            select statement
            //        );
            //    }
            //    else
            //    {
            //        query = query.Where(x => x.Object.ObjectType == EntityObjectType.Activity && x.Object.Activity.Hash == activityHash);
            //    }
            //}

            //if (request.Registration.HasValue)
            //{
            //    Guid registration = request.Registration.Value;
            //    query = (
            //        from statement in query
            //        where statement.Context != null && statement.Context.Registration == registration
            //        select statement
            //    );
            //}

            //if (request.Ascending.GetValueOrDefault())
            //{
            //    query = query.OrderBy(x => x.Stored);
            //}
            //else
            //{
            //    query = query.OrderByDescending(x => x.Stored);
            //}

            //if (request.Since.HasValue)
            //{
            //    query = query.Where(x => x.Stored >= request.Since.Value);
            //}

            //if (request.Until.HasValue)
            //{
            //    query = query.Where(x => x.Stored <= request.Until.Value);
            //}

            //int pageSize = request.Limit ?? 1000;
            //int skipRows = request.PageIndex * size;

            //IQueryable<PagedQuery> pagedQuery = null;

            //if (!request.Attachments.GetValueOrDefault())
            //{
            //    pagedQuery = query.Select(p => new PagedQuery { 
            //        Statement = new StatementEntity
            //        {
            //            StatementId = p.StatementId,
            //            FullStatement = p.FullStatement
            //        },
            //        TotalCount = query.LongCount()
            //    });
            //}
            //else
            //{
            //    pagedQuery = query.Select(p => new PagedQuery
            //    { 
            //        Statement = new StatementEntity
            //        {
            //            StatementId = p.StatementId,
            //            FullStatement = p.FullStatement,
            //            Attachments = p.Attachments,
            //        },
            //        TotalCount = query.LongCount()
            //    });
            //}

            //var result = await pagedQuery.Skip(fromIndex).Take(size)
            //    .ToListAsync(cancellationToken);

            //if (result == null)
            //{
            //    return new PagedStatementsResult();
            //}

            //long totalCount = result.FirstOrDefault()?.TotalCount ?? 0;
            //int totalPagesIndex = (int)Math.Floor((double)totalCount / size);

            //List<Statement> statements = result.Select(p => _mapper.Map<Statement>(p.Statement)).ToList();

            var statementCollection = new StatementCollection(statements);

            if ((fromIndex + size) < 100000)
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

                return new PagedStatementsResult(statementCollection, request.MoreToken);
            }

            return new PagedStatementsResult(statementCollection);
        }

        private async Task<PagedStatementsQuery> GetRequestFromCache(PagedStatementsQuery request, CancellationToken cancellationToken)
        {
            string token = request.MoreToken;
            string jsonString = await _distributedCache.GetStringAsync(token, cancellationToken);
            request = PagedStatementsQuery.FromJson(jsonString);
            request.MoreToken = null;
            return request;
        }
    }
}
