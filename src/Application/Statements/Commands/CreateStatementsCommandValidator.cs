using FluentValidation;

namespace Doctrina.Application.Statements.Commands
{
    public class CreateStatementsCommandValidator : AbstractValidator<CreateStatementsCommand>
    {
        public CreateStatementsCommandValidator()
        {
            // Too much validation, each statement is validated through CreateStatementCommand
            //RuleForEach(x => x.Statements).SetValidator(new StatementValidator());
        }
    }
}
