using System;

namespace Doctrina.Domain.Models.Interfaces
{
    public interface IAgentEntity
    {
        Guid Id { get; set; }
        string Name { get; set; }
        PersonaModel Persona { get; set; }
    }
}
