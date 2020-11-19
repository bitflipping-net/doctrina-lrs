using Doctrina.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public class SubStatementEntity : IStatementObjectEntity, IStatementBaseEntity
    {
        public SubStatementEntity()
        {
            Attachments = new HashSet<AttachmentEntity>();
        }

        public Guid SubStatementId { get; set; }

        //public Guid SubStatementId { get; set; }
        public Guid VerbId { get; set; }
        public VerbEntity Verb { get; set; }
        public Guid ActorId { get; set; }
        public AgentEntity Actor { get; set; }
        public EntityObjectType ObjectType { get; set; }
        public Guid ObjectId { get; set; }
        public object Object { get; set; }
        public Guid? ContextId { get; set; }
        public ContextEntity Context { get; set; }
        public Guid? ResultId { get; set; }
        public ResultEntity Result { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public virtual ICollection<AttachmentEntity> Attachments { get; set; }
    }
}
