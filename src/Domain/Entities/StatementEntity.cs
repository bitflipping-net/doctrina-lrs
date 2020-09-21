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
        public Guid StatementId { get; set; }

        /// <summary>
        /// The actor of the statement (IFI)
        /// </summary>
        public Persona Actor { get; set; }

        /// <summary>
        /// JSON representation of the verb
        /// </summary>
        public string Verb { get; set; }

        /// <summary>
        /// JSON representation of the statement object
        /// </summary>
        public StatementObject Object { get; set; }

        /// <summary>
        /// JSON representation of the statement context
        /// </summary>
        public ContextEntity Context { get; set; }

        /// <summary>
        /// JSON representation of the statement result
        /// </summary>
        public ResultEntity Result { get; set; }

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

        /// <summary>
        /// The xAPI version
        /// </summary>
        public string Version { get; set; }

        #region Navigation Properties
        public virtual StatementEntity VoidingStatement { get; set; }
        public virtual ICollection<AttachmentEntity> Attachments { get; set; }
        #endregion

    }
}
