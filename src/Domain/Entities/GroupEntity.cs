using Doctrina.Domain.Entities.Interfaces;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{

    public class GroupEntity : AgentEntity, IGroupEntity
    {
        public GroupEntity()
        {
            Members = new HashSet<GroupMemberEntity>();
        }

        public override Entities.ObjectType ObjectType => Entities.ObjectType.Group;

        public virtual ICollection<GroupMemberEntity> Members { get; set; }
    }
}
