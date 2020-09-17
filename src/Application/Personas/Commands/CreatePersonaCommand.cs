using Doctrina.Domain.Entities;
using MediatR;

namespace Doctrina.Application.Personas.Commands
{
    public class CreatePersonaCommand : IRequest<Persona>
    {
        public string Name { get; private set; }

        public static CreatePersonaCommand Create(string name)
        {
            return new CreatePersonaCommand()
            {
                Name = name
            };
        }
    }
}
