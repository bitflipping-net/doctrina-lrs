using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public class GroupEntity : AgentEntity
    {
        public GroupEntity()
        {
            Members = new HashSet<AgentEntity>();
        }

        public virtual ICollection<AgentEntity> Members { get; set; }

        public bool IsAnonymous()
        {
            return GetInverseFunctionalIdentifiers().Count == 0;
        }
    }
}
