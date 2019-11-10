using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{

    public class GroupEntity : AgentEntity, IGroupEntity
    {
        public override EntityObjectType ObjectType { get; set; } = EntityObjectType.Group;

        public GroupEntity()
        {
            Members = new HashSet<AgentEntity>();
        }

        public virtual ICollection<AgentEntity> Members { get; set; }
    }
}
