using FluentValidation;

namespace Doctrina.Application.Verbs.Commands
{
    public class MergeVerbCommandValidator : AbstractValidator<UpsertVerbCommand>
    {
        public MergeVerbCommandValidator()
        {
            RuleFor(x => x.Verb.Id).NotEmpty();
        }
    }
}
