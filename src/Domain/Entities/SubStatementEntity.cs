﻿using Doctrina.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public class SubStatementEntity : IStatementObjectEntity, IStatementEntityBase, ISubStatementEntity
    {
        public SubStatementEntity()
        {
            Attachments = new HashSet<AttachmentEntity>();
        }

        public ObjectType ObjectType => ObjectType.SubStatement;

        //public Guid SubStatementId { get; set; }
        public VerbEntity Verb { get; set; }
        public AgentEntity Actor { get; set; }
        public IStatementObjectEntity Object { get; set; }
        public ContextEntity Context { get; set; }
        public ResultEntity Result { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ICollection<AttachmentEntity> Attachments { get; set; }
    }
}
