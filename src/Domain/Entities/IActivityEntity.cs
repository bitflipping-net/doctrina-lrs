using System;

namespace Doctrina.Domain.Entities
{
    public interface IActivityEntity
    {
        Guid ActivityId { get; set; }
        string Hash { get; set; }
        string Id { get; set; }
        ActivityDefinitionEntity Definition { get; set; }
    }
}