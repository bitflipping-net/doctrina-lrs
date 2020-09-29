using Doctrina.Domain.Models;
using MediatR;

namespace Doctrina.Application.Agents.Notifications
{
    public class AgentUpdated : INotification
    {
        public Persona Agent { get; set; }

        public AgentUpdated()
        {
        }

        public static AgentUpdated Create(Persona agent)
        {
            return new AgentUpdated()
            {
                Agent = agent
            };
        }
    }
}
