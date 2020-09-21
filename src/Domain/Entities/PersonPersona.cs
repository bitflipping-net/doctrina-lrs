using System;

namespace Doctrina.Domain.Entities
{
    public class PersonPersona
    {
        public Guid PersonId { get; set; }
        public Person Person { get; set; }

        public Guid PersonaId { get; set; }
        public Persona Persona { get; set; }
    }
}
