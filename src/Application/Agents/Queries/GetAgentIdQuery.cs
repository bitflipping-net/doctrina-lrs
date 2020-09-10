using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.Agents.Queries
{
    public class GetAgentIdQuery : IRequest<Guid?>
    {
        public Agent Agent { get; private set; }

        public static GetAgentIdQuery Create(Agent actor)
        {
            return new GetAgentIdQuery()
            {
                Agent = actor
            };
        }
    }
}