using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public class Persona
    {
        public Guid PersonaId { get; set; }

        public Guid OrganisationId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PersonaIdentifier> Identifiers { get; set; }
    }
}
