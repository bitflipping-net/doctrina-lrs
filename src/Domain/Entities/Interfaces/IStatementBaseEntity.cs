using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public interface IStatementBaseEntity
    {
        AgentEntity Actor { get; set; }
        VerbEntity Verb { get; set; }
        ContextEntity Context { get; set; }
        StatementObjectEntity Object { get; }
        ResultEntity Result { get; set; }
        DateTimeOffset? Timestamp { get; set; }
        ICollection<AttachmentEntity> Attachments { get; set; }
    }
}