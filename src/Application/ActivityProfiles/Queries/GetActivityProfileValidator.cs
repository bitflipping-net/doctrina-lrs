using FluentValidation;

namespace Doctrina.Application.ActivityProfiles.Queries
{
    public class GetActivityProfileValidator : AbstractValidator<GetActivityProfileQuery>
    {
        public GetActivityProfileValidator()
        {
            RuleFor(x => x.ProfileId).NotEmpty();
            RuleFor(x => x.ActivityId).NotEmpty();
        }
    }
}
