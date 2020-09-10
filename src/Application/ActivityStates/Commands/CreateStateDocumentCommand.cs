using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using System;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class CreateStateDocumentCommand : IRequest<ActivityStateDocument>
    {
        public string StateId { get; set; }
        public AgentEntity Agent { get; set; }
        public Guid? Registration { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public ActivityEntity Activity { get; set; }
    }
}
