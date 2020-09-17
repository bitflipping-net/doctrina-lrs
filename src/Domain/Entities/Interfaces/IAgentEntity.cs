using System;

namespace Doctrina.Domain.Entities.Interfaces
{
    public interface IAgentEntity
    {
        Guid AgentId { get; set; }
        string Name { get; set; }
        PersonaIdentifier IFI { get; set; }
    }
}
