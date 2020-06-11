using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;


namespace Doctrina.Application.Agents.Commands
{
    public class UpsertActorCommand : IRequest<AgentEntity>
    {
        public Agent Actor { get; private set; }

        public static UpsertActorCommand Create(Agent agent)
        {
            return new UpsertActorCommand()
            {
                Actor = agent
            };
        }
    }
}
