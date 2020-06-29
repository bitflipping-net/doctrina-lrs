using System.Threading;
using System.Threading.Tasks;
using Doctrina.Application.Agents.Queries;
using Doctrina.Application.Common.Caching;
using Doctrina.Domain.Entities;
using MediatR;

namespace Doctrina.Application.Agents.Notifications
{
    public class GetAgentQueryCacheInvalidator : INotificationHandler<AgentUpdated>
    {
        private readonly ICache<GetAgentQuery, AgentEntity> _cache;

        public GetAgentQueryCacheInvalidator(ICache<GetAgentQuery, AgentEntity> cache)
        {
            _cache = cache;
        }

        public Task Handle(AgentUpdated notification, CancellationToken cancellationToken)
        {
            // _cache.Remove(notification.);
            return Task.CompletedTask;
        }
    }
}