using Doctrina.Domain.Entities;
using MediatR;

namespace Doctrina.Application.Agents.Notifications
{
    public class AgentUpdated : INotification
    {
        public AgentEntity Agent { get; set; }

        public AgentUpdated()
        {
        }

        public static AgentUpdated Create(AgentEntity agent)
        {
            return new AgentUpdated()
            {
                Agent = agent
            };
        }
    }
}