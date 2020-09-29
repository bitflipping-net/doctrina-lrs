using Doctrina.Domain.Models.Interfaces;
using System;

namespace Doctrina.Domain.Models
{
    public class ActivityModel : IStatementObjectModel, IStoreEntity
    {
        public Models.ObjectType ObjectType => Models.ObjectType.Activity;

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
        /// Hash of <see cref="Iri"/>, used for quering
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// The Iri of the Activity.
        /// </summary>
        public string Iri { get; set; }

        public ActivityDefinitionEntity Definition { get; set; }
        
    }
}
