using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using System;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class CreateStateDocumentCommand : IRequest<ActivityStateDocument>
    {
        public string StateId { get; set; }
        public Persona PersonaIdentifier { get; set; }
        public Guid? RegistrationId { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public ActivityModel Activity { get; set; }
    }
}
