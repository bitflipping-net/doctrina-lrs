using FluentValidation;

namespace Doctrina.Application.Statements.Commands
{
    public class VoidStatementCommandValidator : AbstractValidator<VoidStatementCommand>
    {
        public VoidStatementCommandValidator()
        {
            // When issuing a Statement that voids another, the Object of that voiding Statement MUST have the "objectType" property set to StatementRef.
            RuleFor(x => x.Statement.Object.ObjectType).Equal(Domain.Entities.EntityObjectType.StatementRef)
                .WithMessage("When issuing a Statement that voids another, the Object of that voiding Statement MUST have the 'objectType' property set to StatementRef.");

            // An LRS MUST consider a Statement it contains voided if and only if the Statement is not itself a voiding Statement and the LRS also contains a voiding Statement referring to the first Statement.
            RuleFor(x => x.Statement.Verb.Id).NotEqual(ExperienceApi.Data.Verbs.Voided)
                .WithMessage("Any Statement that voids another must have the verb: \"" + ExperienceApi.Data.Verbs.Voided + "\"");
        }
    }
}
