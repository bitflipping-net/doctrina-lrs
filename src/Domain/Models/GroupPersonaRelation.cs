using System;

namespace Doctrina.Domain.Models
{
    public class GroupPersonaRelation
    {
        public Guid GroupId { get; set; }
        public PersonaGroup Group { get; set; }

        public Guid PersonaId { get; set; }
        public PersonaModel Persona { get; set; }
    }
}
