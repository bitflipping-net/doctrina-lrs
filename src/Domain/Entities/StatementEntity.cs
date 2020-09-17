using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public class StatementEntity
    {
        public StatementEntity()
        {
            Attachments = new HashSet<AttachmentEntity>();
        }

        /// <summary>
        /// The primary key
        /// </summary>
        public int StatementId { get; set; }

        /// <summary>
        /// The id of the statement
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The actor of the statement (IFI)
        /// </summary>
        public PersonaIdentifier Actor { get; set; }

        /// <summary>
        /// JSON representation of the verb
        /// </summary>
        public string Verb { get; set; }

        /// <summary>
        /// JSON representation of the statement object
        /// </summary>
        public string Object { get; set; }

        /// <summary>
        /// JSON representation of the statement context
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// JSON representation of the statement result
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// The date and time this statement was stamped.
        /// </summary>
        public DateTimeOffset? Timestamp { get; set; }

        /// <summary>
        /// The date and time this statement was created
        /// </summary>
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// JSON representation of the authority
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// The Id of the statement that voided this statement
        /// </summary>
        public Guid? VoidingStatementId { get; set; }

        /// <summary>
        /// The store this statements belongs to
        /// </summary>
        public Guid StoreId { get; set; }

        #region Navigation Properties
        public virtual StatementEntity VoidingStatement { get; set; }
        public virtual ICollection<AttachmentEntity> Attachments { get; set; }
        #endregion

    }
}
