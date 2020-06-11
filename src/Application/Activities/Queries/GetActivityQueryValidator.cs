using FluentValidation;

namespace Doctrina.Application.Activities.Queries
{
    public class GetActivityQueryValidator : AbstractValidator<GetActivityQuery>
    {
        public GetActivityQueryValidator()
        {
            RuleFor(x=> x.ActivityId).NotEmpty();
        }
    }
}