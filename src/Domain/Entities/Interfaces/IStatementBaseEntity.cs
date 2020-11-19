using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities.Interfaces
{
    public interface IStatementBaseEntity
    {
        AgentEntity Actor { get; set; }
        VerbEntity Verb { get; set; }
        Guid? ContextId { get; set; }
        ContextEntity Context { get; set; }
        EntityObjectType ObjectType { get; set; }
        Guid ObjectId { get; set; }
        Guid? ResultId { get; set; }
        ResultEntity Result { get; set; }
        DateTimeOffset? Timestamp { get; set; }
        ICollection<AttachmentEntity> Attachments { get; set; }
        Guid VerbId { get; set; }
        Guid ActorId { get; set; }
    }
}