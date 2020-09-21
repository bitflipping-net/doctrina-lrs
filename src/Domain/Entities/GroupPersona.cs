using Doctrina.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{

    public class GroupPersona : Persona
    {
        public GroupPersona()
        {
            Personas = new HashSet<Persona>();
        }

        public virtual ICollection<Persona> Personas { get; set; }
    }
}
