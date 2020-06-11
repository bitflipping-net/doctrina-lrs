using Doctrina.ExperienceApi.Data.Validation;
using FluentValidation;

namespace Doctrina.Application.Agents.Queries
{
    public class GetAgentQueryValidator : AbstractValidator<GetAgentQuery>
    {
        public GetAgentQueryValidator(){
            RuleFor(x=> x.Agent).NotEmpty();
        }
    }
}