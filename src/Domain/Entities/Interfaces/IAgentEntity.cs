using System;

namespace Doctrina.Domain.Entities.Interfaces
{
    public interface IAgentEntity
    {
        Guid Id { get; set; }
        string Name { get; set; }
        Persona Persona { get; set; }
    }
}
