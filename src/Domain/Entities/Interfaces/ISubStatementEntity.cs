using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities.Interfaces
{
    public interface ISubStatementEntity
    {
        VerbEntity Verb { get; set; }
        AgentEntity Actor { get; set; }
        StatementObjectEntity Object { get; set; }
        ContextEntity Context { get; set; }
        ResultEntity Result { get; set; }
        DateTimeOffset? Timestamp { get; set; }
        ICollection<AttachmentEntity> Attachments { get; set; }
    }
}
