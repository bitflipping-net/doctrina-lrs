using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public abstract class StatementBaseEntity : StatementObjectBaseEntity, IStatementBaseEntity
    {
        public StatementBaseEntity()
        {
            Attachments = new HashSet<AttachmentEntity>();
        }

        public Guid StatementBaseId { get; set; }
        public AgentEntity Actor { get; set; }
        public StatementObjectBaseEntity Object { get; set; }
        public VerbEntity Verb { get; set; }
        public ResultEntity Result { get; set; }
        public ContextEntity Context { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public virtual ICollection<AttachmentEntity> Attachments { get; set; }
    }
}
