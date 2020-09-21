using System;

namespace Doctrina.Domain.Entities.Documents
{
    public class AgentProfileEntity : DocumentEntity, IAgentProfileEntity
    {
        public AgentProfileEntity()
        {
        }

        public AgentProfileEntity(byte[] content, string contentType) : base(content, contentType)
        {
        }
    }
}
