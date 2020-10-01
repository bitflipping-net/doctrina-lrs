using Doctrina.Domain.Models;
using MediatR;

namespace Doctrina.Application.Agents.Notifications
{
    public class AgentUpdated : INotification
    {
        public PersonaModel Agent { get; set; }

        public AgentUpdated()
        {
        }

        public static AgentUpdated Create(PersonaModel agent)
        {
            return new AgentUpdated()
            {
                Agent = agent
            };
        }
    }
}
