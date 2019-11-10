using System;

namespace Doctrina.Domain.Entities.Documents
{
    public class AgentProfileEntity : IAgentProfileEntity
    {
        public AgentProfileEntity()
        {
        }

        public AgentProfileEntity(byte[] content, string contentType)
        {
            Document = new DocumentEntity(content, contentType);
        }

        public Guid AgentProfileId { get; set; }
        public string ProfileId { get; set; }
        public virtual AgentEntity Agent { get; set; }
        public DocumentEntity Document { get; set; }
    }
}
