using Doctrina.ExperienceApi.Data;
using FluentValidation;

namespace Doctrina.Application.Statements.Queries
{
    public class PagedStatementsQueryValidator : AbstractValidator<PagedStatementsQuery>
    {
        public PagedStatementsQueryValidator()
        {
            RuleFor(x => x)
                .Must(g => !(g.StatementId.HasValue && g.VoidedStatementId.HasValue))
                .WithMessage("VoidedStatementId and StatementId parameters cannot be used together.");

            RuleFor(x => x)
                .Must(ValidateParameters)
                .When(x => x.StatementId.HasValue)
                .WithMessage("Only attachments and format parameters are allowed with using statementId");


            RuleFor(x => x)
                .Must(ValidateParameters)
                .When(x => x.VoidedStatementId.HasValue)
                .WithMessage("Only attachments and format parameters are allowed with using voidedStatementId");
        }

        private static bool ValidateParameters(PagedStatementsQuery parameters)
        {
            var otherParameters = parameters.ToParameterMap(ApiVersion.GetLatest());
            otherParameters.Remove("attachments");
            otherParameters.Remove("format");
            return otherParameters.Count == 0;
        }
    }
}
