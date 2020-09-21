using Doctrina.Domain.Entities.Interfaces;
using System;

namespace Doctrina.Domain.Entities
{
    public class ActivityEntity : IActivity, IStatementObjectEntity
    {
        public Entities.ObjectType ObjectType => Entities.ObjectType.Activity;

        /// <summary>
        /// The primary key
        /// </summary>
        public Guid ActivityId { get; set; }

        /// <summary>
        /// The id of the <see cref="Store"/>
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// The store this activity is stored in.
        /// </summary>
        public virtual Store Store { get; set; }

        /// <summary>
        /// Hash of <see cref="Id"/>
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Actual absolute Iri
        /// </summary>
        public string Id { get; set; }

        public ActivityDefinitionEntity Definition { get; set; }
        
    }
}
