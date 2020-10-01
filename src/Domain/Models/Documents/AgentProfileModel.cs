using System;

namespace Doctrina.Domain.Models.Documents
{
    public class AgentProfileModel : DocumentModel, IAgentProfileEntity
    {
        public AgentProfileModel()
        {
        }

        public AgentProfileModel(byte[] content, string contentType) : base(content, contentType)
        {
        }

        /// <summary>
        /// The unique id of the profile
        /// </summary>
        public string ProfileId { get; set; }
    }
}
