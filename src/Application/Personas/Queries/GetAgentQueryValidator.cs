using Doctrina.Domain.Models;
using FluentValidation;

namespace Doctrina.Application.Personas.Queries
{
    public class GetAgentQueryValidator : AbstractValidator<GetPersonaQuery>
    {
        public GetAgentQueryValidator()
        {
            RuleFor(x => x.Key).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.ObjectType).NotEqual(ObjectType.Invalid);
        }
    }
}
