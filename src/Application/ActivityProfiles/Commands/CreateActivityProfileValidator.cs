using FluentValidation;

namespace Doctrina.Application.ActivityProfiles.Commands
{
    public class CreateActivityProfileValidator : AbstractValidator<CreateActivityProfileCommand>
    {
        public CreateActivityProfileValidator()
        {
            RuleFor(x => x.ProfileId).NotEmpty();
            RuleFor(x => x.ActivityId).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.ContentType).NotEmpty();
        }
    }
}
