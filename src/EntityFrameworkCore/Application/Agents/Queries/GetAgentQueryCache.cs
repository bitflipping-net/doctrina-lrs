using Doctrina.Application.Common.Caching;
using Doctrina.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Doctrina.Application.Agents.Queries
{
    public class GetAgentQueryCache : MemoryCache<GetAgentQuery, AgentEntity>
    {
        /// <summary>
        /// Each time the cached response is retrieved another 30 minutes will be added to the time before the cached
        /// response expires.
        /// </summary>
        protected override TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(30);

        public GetAgentQueryCache(IMemoryCache memoryCache)
        : base(memoryCache)
        {
        }

        protected override string GetCacheKeyIdentifier(GetAgentQuery request)
        {
            return request.Agent.GetAgentCacheKeyIdentifier();
        }
    }
}