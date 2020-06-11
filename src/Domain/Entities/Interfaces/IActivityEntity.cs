using System;

namespace Doctrina.Domain.Entities.Interfaces
{
    public interface IActivity
    {
        Guid ActivityId { get; set; }
        string Hash { get; set; }
        string Id { get; set; }
        ActivityDefinitionEntity Definition { get; set; }
    }
}