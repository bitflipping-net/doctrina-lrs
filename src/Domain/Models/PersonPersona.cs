using System;

namespace Doctrina.Domain.Models
{
    public class PersonPersona
    {
        public Guid PersonId { get; set; }
        public PersonModel Person { get; set; }

        public Guid PersonaId { get; set; }
        public PersonaModel Persona { get; set; }
    }
}
