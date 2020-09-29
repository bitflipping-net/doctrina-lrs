using Doctrina.Domain.Models.Documents;
using Doctrina.Domain.Models.Relations;
using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Models
{
    /// <summary>
    /// Store contains xAPI Statements, Verbs, Activities, Documents
    /// </summary>
    public class Store
    {
        public Store()
        {
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// The primary key for this store.
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// The name of the store
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The date this store was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The date this store was updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Number of statements stored in this store.
        /// </summary>
        public int StatementsCount { get; set; }


        public virtual ICollection<VerbModel> Verbs { get; set; }

        public virtual ICollection<ActivityModel> Activities { get; set; }

        public virtual ICollection<DocumentEntity> Documents { get; set; }

        public virtual ICollection<StatementModel> Statements { get; set; }

        public virtual ICollection<StatementRelation> Relations { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

        public Guid OrganisationId { get; set; }
    }
}
