using Doctrina.ExperienceApi.Data.Validation;
using FluentValidation;

namespace Doctrina.Application.Agents.Queries
{
    public class GetPersonQueryValidator : AbstractValidator<GetPersonQuery>
    {
        public GetPersonQueryValidator()
        {
            RuleFor(x=> x.Agent).SetValidator(new AgentValidator());
        }
    }
}