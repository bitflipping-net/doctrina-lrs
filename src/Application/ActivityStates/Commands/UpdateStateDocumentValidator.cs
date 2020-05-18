using System.Text;
using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Data.Json;
using Doctrina.ExperienceApi.Data.Validation;
using FluentValidation;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class UpdateStateDocumentValidator : AbstractValidator<UpdateStateDocumentCommand>
    {
        public UpdateStateDocumentValidator()
        {
            RuleFor(x=> x.StateId).NotEmpty();
            RuleFor(x=> x.ActivityId).NotEmpty();
            RuleFor(x=> x.Agent).SetValidator(new AgentValidator());
            RuleFor(x=> x.Content).NotEmpty();
            RuleFor(x=> x.ContentType).NotEmpty();

            RuleFor(x => x.Content)
                .Must((content) =>
                {
                    JsonString jsonString = new JsonString(Encoding.UTF8.GetString(content));
                    return jsonString.IsValid();
                })
                .When(x=> x.ContentType == MediaTypes.Application.Json);
        }
    }
}
