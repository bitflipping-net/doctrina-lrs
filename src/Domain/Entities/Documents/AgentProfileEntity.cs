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

        /// <summary>
        /// The primary key
        /// </summary>
        public Guid AgentProfileId { get; set; }

        /// <summary>
        /// The unique id of the profile
        /// </summary>
        public string ProfileId { get; set; }

        public PersonaIdentifier IFI { get; set; }

        public virtual AgentEntity Agent { get; set; }

        public DocumentEntity Document { get; set; }
    }
}
