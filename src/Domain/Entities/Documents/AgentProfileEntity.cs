using System;

namespace Doctrina.Domain.Entities.Documents
{
    public class AgentProfileEntity : DocumentEntity
    {
        public AgentProfileEntity()
        {
        }

        public AgentProfileEntity(byte[] content, string contentType)
            : base(content, contentType)
        {
        }

        public virtual AgentEntity Agent { get; set; }
    }
}
