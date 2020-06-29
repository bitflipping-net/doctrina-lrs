using System;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Agents.Queries;
using Doctrina.Application.Common.Caching;
using Doctrina.Domain.Entities;

namespace Doctrina.Application.Agents.Queries
{
    public class GetAgentQueryCacheInvalidator : CacheInvalidator<UpsertActorCommand, GetAgentQuery, AgentEntity>
    {
        public GetAgentQueryCacheInvalidator(ICache<GetAgentQuery, AgentEntity> cache) : base(cache)
        {
        }

        protected override string GetCacheKeyIdentifier(UpsertActorCommand request)
        {
            return request.Actor.GetAgentCacheKeyIdentifier();
        }
    }
}