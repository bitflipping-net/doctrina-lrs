using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public class Person
    {
        public Person()
        {
            Personas = new HashSet<PersonPersona>();
        }

        public Guid PersonId { get; set; }

        public Guid OrganisationId { get; set; }

        public Organisation Organisation { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PersonPersona> Personas { get; set; }
    }
}
