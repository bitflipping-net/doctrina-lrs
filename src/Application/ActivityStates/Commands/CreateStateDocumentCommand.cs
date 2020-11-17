using Doctrina.Domain.Entities.Documents;
using MediatR;
using System;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class CreateStateDocumentCommand : IRequest<ActivityStateEntity>
    {
        public string StateId { get; set; }
        public Guid AgentId { get; set; }
        public Guid? Registration { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public Guid ActivityId { get; set; }
    }
}
