using Doctrina.Domain.Entities.Interfaces;
using Doctrina.Domain.Entities.ValueObjects;
using System;

namespace Doctrina.Domain.Entities
{
    public class VerbEntity : IVerbEntity
    {
        /// <summary>
        /// The Primary key
        /// </summary>
        public Guid VerbId { get; set; }

        /// <summary>
        /// SHA-1 of <see cref="IRI"/>
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Corresponds to a Verb definition. (IRI)
        /// </summary>
        public string IRI { get; set; }

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
