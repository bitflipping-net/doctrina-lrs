using Doctrina.Domain.Models;
using MediatR;

namespace Doctrina.Application.Persons.Queries
{
    public class GetPersonQuery : IRequest<PersonModel>
    {
        public PersonaModel Persona { get; private set; }

        public static GetPersonQuery Create(PersonaModel persona)
        {
            return new GetPersonQuery()
            {
                Persona = persona
            };
        }
    }
}
