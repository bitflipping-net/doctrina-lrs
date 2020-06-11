using Doctrina.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    [Serializable]
    public class StatementEntity : IStatementBaseEntity, IStatementEntity
    {
        public StatementEntity()
        {
            Attachments = new HashSet<AttachmentEntity>();
        }

        public Guid StatementId { get; set; }
        public Guid ActorId { get; set; }
        public AgentEntity Actor { get; set; }
        public Guid VerbId { get; set; }
        public VerbEntity Verb { get; set; }
        public StatementObjectEntity Object { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ResultEntity Result { get; set; }
        public ContextEntity Context { get; set; }
        public virtual ICollection<AttachmentEntity> Attachments { get; set; }
        public DateTimeOffset? Stored { get; set; }
        public string Version { get; set; }
        public Guid AuthorityId { get; set; }
        public string FullStatement { get; set; }
        public Guid? VoidingStatementId { get; set; }

        #region Navigation Properties
        public AgentEntity Authority { get; set; }
        public StatementEntity VoidingStatement { get; set; }
        #endregion

    }
}
