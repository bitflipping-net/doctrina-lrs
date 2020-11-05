using Doctrina.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Models
{
    /// <summary>
    /// The context of a statement
    /// </summary>
    public class ContextModel : IStoreOwnedEntity
    {
        /// <summary>
        /// The Primary key
        /// </summary>
        public Guid ContextId { get; set; }

        /// <summary>
        /// The registration context id
        /// </summary>
        public Guid? Registration { get; set; }

        /// <summary>
        /// The id of the <see cref="Instructor"/>
        /// </summary>
        public Guid InstructorId { get; set; }

        /// <summary>
        /// The instructor
        /// </summary>
        public PersonaModel Instructor { get; set; }

        /// <summary>
        /// The id of the <see cref="Team"/>
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// The team
        /// </summary>
        public PersonaModel Team { get; set; }

        /// <summary>
        /// The revision
        /// </summary>
        public string Revision { get; set; }

        /// <summary>
        /// The platform
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// The language
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// The extensions
        /// </summary>
        public ExtensionsCollection Extensions { get; set; }

        /// <summary>
        /// The id of the <see cref="Store"/>
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// The store this statement context belongs to
        /// </summary>
        public Store Store { get; set; }

        /// <summary>
        /// The id of the <see cref="Statement"/> this context belongs to
        /// </summary>
        public Guid StatementId { get; set; }

        /// <summary>
        /// The statement this belongs to.
        /// </summary>
        public StatementModel Statement {get;set;}

        /// <summary>
        /// The context activities connected to this statement's context
        /// </summary>
        public virtual ICollection<ContextActivity> ContextActivities { get; set; }
    }
}
