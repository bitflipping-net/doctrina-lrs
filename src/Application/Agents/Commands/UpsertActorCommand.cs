using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using MediatR;


namespace Doctrina.Application.Agents.Commands
{
    public class UpsertActorCommand : IRequest<Persona>
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
