using FluentValidation;

namespace Doctrina.Application.Persons.Queries
{
    public class GetPersonQueryValidator : AbstractValidator<GetPersonQuery>
    {
        public GetPersonQueryValidator()
        {
            RuleFor(x => x.AgentId).NotEmpty();
        }
    }
}