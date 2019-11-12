using Doctrina.Domain.Entities.Interfaces;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{

    public class GroupEntity : AgentEntity, IGroupEntity
    {
        public GroupEntity()
        {
            Members = new HashSet<AgentEntity>();
            ObjectType = EntityObjectType.Group;
        }

        public override EntityObjectType ObjectType { get; set; }

        public virtual ICollection<AgentEntity> Members { get; set; }
    }
}
