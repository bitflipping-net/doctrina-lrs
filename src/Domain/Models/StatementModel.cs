using System;
using System.Collections.Generic;
using Doctrina.Domain.Models.Relations;

namespace Doctrina.Domain.Models
{
    public class StatementModel : StatementBaseModel
    {
        public StatementModel()
        {
            Attachments = new HashSet<AttachmentEntity>();
        }

        /// <summary>
        /// The id property of the statement (stamped)
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The id of the <see cref="Client"/>
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// The client who was authorized to store this statement.
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// The date and time when this statement was voided. 
        /// </summary>
        public DateTimeOffset? VoidedAt { get; set; }

        /// <summary>
        /// The Id of the <see cref="VoidingStatement"/>
        /// </summary>
        public int? VoidingStatementId { get; set; }

        /// <summary>
        /// The voiding statement
        /// </summary>
        public virtual StatementModel VoidingStatement { get; set; }

        /// <summary>
        /// The encoded statement
        /// </summary>
        public virtual StatementEncoded Encoded { get; set; }
    }
}
