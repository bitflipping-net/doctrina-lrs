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
        public virtual AgentEntity Actor { get; set; }
        public Guid VerbId { get; set; }
        public virtual VerbEntity Verb { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public virtual ResultEntity Result { get; set; }
        public virtual ContextEntity Context { get; set; }
        public virtual ICollection<AttachmentEntity> Attachments { get; set; }
        public DateTimeOffset? Stored { get; set; }
        public string Version { get; set; }
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }
        public string FullStatement { get; set; }
        public Guid? VoidingStatementId { get; set; }
        public StatementEntity VoidingStatement { get; set; }
        public EntityObjectType ObjectType { get; set; }
        public Guid ObjectId { get; set; }
        public object Object { get; set; }
    }
}
