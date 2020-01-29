using Doctrina.ExperienceApi.Data.Validation;
using FluentValidation;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class MergeStateDocumentValidator : AbstractValidator<MergeStateDocumentCommand>
    {
        public MergeStateDocumentValidator()
        {
            RuleFor(x=> x.StateId).NotEmpty();
            RuleFor(x=> x.ActivityId).NotEmpty();
            RuleFor(x=> x.Agent).SetValidator(new AgentValidator());
            RuleFor(x=> x.Content).NotEmpty();
            RuleFor(x=> x.ContentType).NotEmpty();
        }
    }
}
