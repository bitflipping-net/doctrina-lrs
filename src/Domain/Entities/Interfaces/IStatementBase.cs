using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public interface IStatementEntityBase
    {
        AgentEntity Actor { get; set; }
        VerbEntity Verb { get; set; }
        StatementObjectEntity Object { get; set; }
        ContextEntity Context { get; set; }
        ResultEntity Result { get; set; }
        DateTimeOffset? Timestamp { get; set; }
        ICollection<AttachmentEntity> Attachments { get; set; }
    }
}
