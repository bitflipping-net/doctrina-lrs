using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.Clients.Queries
{
    public class ClientByAgentQuery : IRequest<Client>
    {
        public Agent Agent { get; private set; }

        public static ClientByAgentQuery Create(Agent agent)
        {
            return new ClientByAgentQuery()
            {
                Agent = agent
            };
        }
    }
}
