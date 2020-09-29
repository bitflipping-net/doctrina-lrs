using System;
using System.Collections.Generic;
using Doctrina.Domain.Models.Interfaces;

namespace Doctrina.Domain.Models
{
    public class StatementBaseModel : IStoreEntity, IStatementEntityBase
    {
        public StatementBaseModel()
        {
            Attachments = new HashSet<AttachmentEntity>();
        }

        /// <summary>
        /// The primary key
        /// </summary>
        public Guid StatementId { get; set; }

        public Guid PersonaId { get; set; }

        /// <summary>
        /// The actor of the statement (IFI)
        /// </summary>
        public Persona Persona { get; set; }

        public Guid VerbId { get; set; }

        /// <summary>
        /// The verb
        /// </summary>
        public VerbModel Verb { get; set; }

        /// <summary>
        /// The ObjectType of the object of the Statement or its SubStatement
        /// </summary>
        public ObjectType ObjectType { get; set; }

        /// <summary>
        /// Guid of the related Object (Agent, Group, Activity, Statement)
        /// </summary>
        public Guid ObjectId { get; set; }

        /// <summary>
        /// Context
        /// </summary>
        public StatementContext Context { get; set; }

        /// <summary>
        /// Result 
        /// </summary>
        public ResultModel Result { get; set; }

        /// <summary>
        /// The date and time this statement was stamped.
        /// </summary>
        public DateTimeOffset? Timestamp { get; set; }

        /// <summary>
        /// The date and time this statement was created
        /// </summary>
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// The store this statements belongs to
        /// </summary>
        public Guid StoreId { get; set; }

        public ICollection<AttachmentEntity> Attachments { get; set; }
    }
}
