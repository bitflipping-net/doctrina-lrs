using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.Agents.Queries
{
    public class GetAgentQuery : IRequest<Persona>
    {
        public Agent Agent { get; set; }

        public static GetAgentQuery Create(Agent actor)
        {
            return new GetAgentQuery()
            {
                Agent = actor
            };
        }
    }
}
