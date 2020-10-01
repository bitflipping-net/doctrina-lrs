using Doctrina.Application.Personas;
using FluentValidation;

namespace Doctrina.Application.Persons.Queries
{
    public class GetPersonQueryValidator : AbstractValidator<GetPersonQuery>
    {
        public GetPersonQueryValidator()
        {
            RuleFor(x => x.Persona).SetValidator(new PersonaModelValidator());
        }
    }
}
