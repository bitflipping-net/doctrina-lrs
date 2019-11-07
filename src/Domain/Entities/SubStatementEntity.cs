using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public class SubStatementEntity : IStatementObjectEntity, IStatementEntityBase
    {
        public SubStatementEntity()
        {
            Attachments = new HashSet<AttachmentEntity>();
        }

        public EntityObjectType ObjectType => EntityObjectType.SubStatement;

        //public Guid SubStatementId { get; set; }
        public VerbEntity Verb { get; set; }
        public AgentEntity Actor { get; set; }
        public StatementObjectEntity Object { get; set; }
        public ContextEntity Context { get; set; }
        public ResultEntity Result { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ICollection<AttachmentEntity> Attachments { get; set; }
    }
}
