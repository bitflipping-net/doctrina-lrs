using Doctrina.Application.Statements.Queries;
using Doctrina.ExperienceApi.Data.Validation;
using FluentValidation;
using MediatR;

namespace Doctrina.Application.Statements.Commands
{
    public class PutStatementCommandValidator : AbstractValidator<PutStatementCommand>
    {
        private readonly IMediator _mediator;

        public PutStatementCommandValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(x => x.StatementId).NotEmpty();
            RuleFor(x => x.Statement).SetValidator(new StatementValidator());

            RuleFor(x => x).Must((request) =>
            {
                return !request.Statement.Id.HasValue ||
                    request.StatementId == request.Statement.Id.Value;
            })
                .WithErrorCode("409")
                .WithMessage("The 'id' property of the Statement MUST match the 'statementId' parameter of the request.")
                .WithName("request");

            RuleFor(x => x).MustAsync(async (request, cancellationToken) =>
            {
                var savedStatement = await _mediator.Send(GetStatementQuery.Create(request.StatementId), cancellationToken);

                return savedStatement == null || !savedStatement.Equals(request.Statement);
            })
                .WithErrorCode("409")
                .WithMessage("A stored statement with the of 'statementId' parameter doest not match request statement.")
                .WithName("request");
        }
    }
}
