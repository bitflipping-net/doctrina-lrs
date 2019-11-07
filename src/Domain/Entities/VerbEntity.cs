using Doctrina.Domain.Entities.OwnedTypes;
using System.Collections.Generic;
using System;

namespace Doctrina.Domain.Entities
{
    public class VerbEntity
    {
        /// <summary>
        /// Entity Id of the Verb
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

        public LanguageMapCollection Display { get; set; }
    }
}
