using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Models.Interfaces
{
    public interface IStatementEntityBase
    {
        Persona Persona { get; set; }
        VerbModel Verb { get; set; }
        StatementContext Context { get; set; }
        ResultModel Result { get; set; }
        DateTimeOffset? Timestamp { get; set; }
        ICollection<AttachmentEntity> Attachments { get; set; }
    }
}
