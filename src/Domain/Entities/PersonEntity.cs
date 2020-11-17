using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    /// <summary>
    /// A person object
    /// </summary>
    public class PersonEntity
    {
        public Guid PersonId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AgentEntity> Agents { get; set; }
    }
}
