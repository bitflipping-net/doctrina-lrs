using System;

namespace Doctrina.Domain.Entities.Documents
{
    public interface IActivityStateEntity
    {
        ActivityEntity Activity { get; set; }
        AgentEntity Agent { get; set; }
        Guid? Registration { get; set; }
        string StateId { get; set; }
        DocumentEntity Document { get; set; }

    }
}