using System.Collections.Generic;

namespace Doctrina.Domain.Models
{
    /// <summary>
    /// Group of personas
    /// </summary>
    public class PersonaGroup : PersonaModel
    {
        public PersonaGroup()
        {
            Members = new HashSet<GroupPersonaRelation>();
        }

        public virtual ICollection<GroupPersonaRelation> Members { get; set; }
    }
}
