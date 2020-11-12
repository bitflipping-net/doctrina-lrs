using Doctrina.ExperienceApi.Data.Http;
using Doctrina.ExperienceApi.Data.Json;
using FluentValidation;
using System.Text;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class UpdateStateDocumentValidator : AbstractValidator<UpdateStateDocumentCommand>
    {
        public UpdateStateDocumentValidator()
        {
            RuleFor(x => x.StateId).NotEmpty();
            RuleFor(x => x.ActivityId).NotEmpty();
            RuleFor(x => x.AgentId).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.ContentType).NotEmpty();

            RuleFor(x => x.Content)
                .Must((content) =>
                {
                    JsonString jsonString = new JsonString(Encoding.UTF8.GetString(content));
                    return jsonString.IsValid();
                })
                .When(x => x.ContentType == MediaTypes.Application.Json);
        }
    }
}
