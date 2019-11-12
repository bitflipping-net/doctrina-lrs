using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities.Interfaces
{
    public interface IStatementEntity
    {
        Guid StatementId { get; set; }
        AgentEntity Actor { get; set; }
        VerbEntity Verb { get; set; }
        StatementObjectEntity Object { get; set; }
        DateTimeOffset? Timestamp { get; set; }
        ResultEntity Result { get; set; }
        ContextEntity Context { get; set; }
        ICollection<AttachmentEntity> Attachments { get; set; }
        DateTimeOffset? Stored { get; set; }
        string Version { get; set; }
        Guid? AuthorityId { get; set; }
        string FullStatement { get; set; }
        bool Voided { get; set; }
        AgentEntity Authority { get; set; }
    }
}
