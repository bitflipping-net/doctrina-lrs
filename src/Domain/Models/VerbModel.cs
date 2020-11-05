using Doctrina.Domain.Models.Interfaces;
using Doctrina.Domain.Models.ValueObjects;
using System;

namespace Doctrina.Domain.Models
{
    public class VerbModel : IVerbModel, IStoreOwnedEntity
    {
        /// <summary>
        /// The Primary Key
        /// </summary>
        public Guid VerbId { get; set; }

        /// <summary>
        /// SHA-1 of <see cref="Id"/>
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Corresponds to a Verb definition. (IRI)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// JSON encoded string of the language maps
        /// </summary>
        public LanguageMapCollection Display { get; set; }

        /// <summary>
        /// The id of the <see cref="Store"/> this verb belongs to.
        /// </summary>
        public Guid StoreId { get; set; }
    }
}
