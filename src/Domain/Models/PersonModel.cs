using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Models
{
    public class PersonModel
    {
        public PersonModel()
        {
            Personas = new HashSet<PersonPersona>();
        }

        public Guid PersonId { get; set; }

        public Guid StoreId { get; set; }

        public Store Store { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PersonPersona> Personas { get; set; }
    }
}
