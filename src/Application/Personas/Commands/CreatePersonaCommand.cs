using Doctrina.Domain.Models;
using MediatR;

namespace Doctrina.Application.Personas.Commands
{
    public class CreatePersonaCommand : IRequest<PersonModel>
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
