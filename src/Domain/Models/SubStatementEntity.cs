using Doctrina.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Models
{
    public class SubStatementEntity : StatementBaseModel, IStatementEntityBase
    {
        public SubStatementEntity()
        {
            Attachments = new HashSet<AttachmentEntity>();
        }
    }
}
