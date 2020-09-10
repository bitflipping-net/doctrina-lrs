using Doctrina.Application.Common.Caching;
using Doctrina.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Doctrina.Application.Statements.Queries
{
    public class StatementQueryCache : MemoryCache<StatementQuery, StatementEntity>
    {
        /// <summary>
        /// Each time the cached response is retrieved another 30 minutes will be added to the time before the cached
        /// response expires.
        /// </summary>
        protected override TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(30);

        public StatementQueryCache(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        protected override string GetCacheKeyIdentifier(StatementQuery request)
        {
            // cache every response where the user id is different
            return $"{request.StatementId.ToString()}_{request.IncludeAttachments}";
        }
    }
}