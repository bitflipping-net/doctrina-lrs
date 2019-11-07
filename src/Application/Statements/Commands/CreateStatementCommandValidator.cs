using Doctrina.Application.Statements.Queries;
using Doctrina.ExperienceApi.Data.Validation;
using FluentValidation;
using MediatR;

namespace Doctrina.Application.Statements.Commands
{
    public class CreateStatementCommandValidator : AbstractValidator<CreateStatementCommand>
    {
        private readonly IMediator _mediator;

        public CreateStatementCommandValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(x => x.Statement).NotNull().SetValidator(new StatementValidator())
                .DependentRules(() =>
                {
                    RuleFor(x => x.Statement).MustAsync(async (statement, cancellationToken) =>
                    {
                        var savedStatement = await _mediator.Send(GetStatementQuery.Create(statement.Id.Value), cancellationToken);

                        return savedStatement == null || statement.Equals(savedStatement);
                    })
                      .WithErrorCode("409")
                      .WithName("id")
                      .WithMessage("A statement is stored with the same id, and it does not match request statement.")
                      .When(x => x.Statement.Id.HasValue);
                });
        }
    }
}
