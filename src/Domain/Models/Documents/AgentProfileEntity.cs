using System;

namespace Doctrina.Domain.Models.Documents
{
    public class AgentProfileEntity : DocumentEntity, IAgentProfileEntity
    {
        public AgentProfileEntity()
        {
        }

        public AgentProfileEntity(byte[] content, string contentType) : base(content, contentType)
        {
        }

        /// <summary>
        /// The unique id of the profile
        /// </summary>
        public string ProfileId { get; set; }
    }
}
