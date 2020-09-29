using AutoMapper;
using Doctrina.Application.Infrastructure.ExperienceApi;
using Doctrina.Application.Statements.Models;
using Doctrina.Domain.Models;
using Doctrina.Persistence;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Queries
{
    public class PagedStatementsQueryHandler : IRequestHandler<PagedStatementsQuery, PagedStatementsResult>
    {
        private readonly IStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public PagedStatementsQueryHandler(IStoreDbContext context, IMapper mapper, IDistributedCache distributedCache)
        {
            _context = context;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<PagedStatementsResult> Handle(PagedStatementsQuery request, CancellationToken cancellationToken)
        {
            var fallback = new PagedStatementsResult();

            if (!string.IsNullOrWhiteSpace(request.MoreToken))
            {
                string token = request.MoreToken;
                string jsonString = await _distributedCache.GetStringAsync(token, cancellationToken);
                request = PagedStatementsQuery.FromJson(jsonString);
                request.MoreToken = null;
            }

            var query = _context.Statements
                .OfType<StatementModel>()
                .AsNoTracking()
                .Where(x => x.StoreId == _context.StoreId);

            if (request.VerbId != null)
            {
                string verbHash = request.VerbId.ComputeHash();
                var verb = await _context.Verbs
                    .Where(x => x.StoreId == _context.StoreId)
                    .SingleOrDefaultAsync(x => x.Hash == verbHash);
                query = query.Where(x => x.VerbId == verb.VerbId);
            }

            if (request.Agent != null)
            {
                Persona persona = await _context.Personas
                    .Where(x => x.StoreId == _context.StoreId)
                    .SingleOrDefaultAsync(x =>
                    x.Key == request.Agent.GetIdentifierKey()
                    && x.Value == request.Agent.GetIdentifierValue());

                if (persona == null)
                    return fallback;

                if (request.RelatedAgents.GetValueOrDefault())
                {
                    query = (
                        from statement in query
                        from relation in _context.Relations
                        where relation.StoreId == _context.StoreId
                        && relation.ParentId == statement.StatementId
                        && (relation.ObjectType == ObjectType.Agent
                            || relation.ObjectType == ObjectType.Group)
                        && relation.ChildId == persona.PersonaId
                        select statement
                    );
                }
                else
                {
                    query = query.Where(x => x.PersonaId == persona.PersonaId);
                }

            }

            if (request.ActivityId != null)
            {
                string activityHash = request.ActivityId.ComputeHash();
                ActivityModel activity = await _context.Activities
                    .Where(x => x.StoreId == _context.StoreId)
                    .SingleOrDefaultAsync(x => x.Hash == activityHash);

                if (activity == null)
                    return fallback;

                if (request.RelatedActivities.GetValueOrDefault())
                {
                    query = (
                        from statement in query
                        from relation in _context.Relations
                        where relation.StoreId == _context.StoreId
                        && relation.ParentId == statement.StatementId
                        && relation.ObjectType == ObjectType.Activity
                        && relation.ChildId == activity.ActivityId
                        select statement
                    );
                }
                else
                {
                    query = query.Where(x => x.ObjectType == ObjectType.Activity
                        && x.ObjectId == activity.ActivityId);
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
                query = query.Where(x => x.CreatedAt >= request.Since.Value);
            }

            if (request.Until.HasValue)
            {
                query = query.Where(x => x.CreatedAt <= request.Until.Value);
            }

            int pageSize = request.Limit ?? 1000;
            int skipRows = request.PageIndex * pageSize;

            IQueryable<StatementModel> pagedQuery = null;

            _context.Statements.FromSqlInterpolated($"PagedStatementsQuery {request.Ascending} {request.Limit}");

            // Include voiding statements
            query = query.Select(p => p.VoidingStatementId != null ? p.VoidingStatement : p);

            if (!request.Attachments.GetValueOrDefault())
            {
                pagedQuery = query.Select(p => new StatementModel
                {
                    StatementId = p.StatementId,
                    Encoded = new StatementEncoded()
                    {
                        Payload = p.Encoded.Payload
                    }
                });
            }
            else
            {
                pagedQuery = query.Select(p => new StatementModel
                {
                    StatementId = p.StatementId,
                    Encoded = new StatementEncoded()
                    {
                        Payload = p.Encoded.Payload
                    },
                    Attachments = p.Attachments,
                });
            }

            var result = await pagedQuery.Skip(skipRows).Take(pageSize + 1)
                .ToListAsync(cancellationToken);

            if (result == null)
            {
                return fallback;
            }

            List<StatementModel> statements = result.Take(pageSize).ToList();

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
