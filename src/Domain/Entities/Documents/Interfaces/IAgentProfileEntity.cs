using System;

namespace Doctrina.Domain.Entities.Documents
{
    public interface IAgentProfileEntity
    {
        string ProfileId { get; set; }
        AgentEntity Agent { get; set; }
        DocumentEntity Document { get; set; }

    }
}