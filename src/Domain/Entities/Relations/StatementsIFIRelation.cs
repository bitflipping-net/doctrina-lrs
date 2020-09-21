using System;

namespace Doctrina.Domain.Entities.Relations
{
    public class StatementIdentifiers
    {
        public Guid StoreId { get; set; }
        public Store Store { get; set; }

        public Guid StatementId { get; set; }
        public StatementEntity Statement { get; set; }

        /// <summary>
        /// The primary of IFI of the <see cref="PersonaIdentifier"/>
        /// </summary>
        public Guid IFI { get; set; }

        /// <summary>
        /// The <see cref="Entities.Persona"/> of ifi id.
        /// </summary>
        public Persona PersonaIdentifier { get; set; }
    }
}
