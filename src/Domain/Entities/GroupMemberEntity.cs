using System;

namespace Doctrina.Domain.Entities
{
    public class GroupMemberEntity
    {
        public Guid GroupMemberId { get; set; }

        public Guid GroupId { get; set; }

        public Guid MemberId { get; set; }

        public virtual AgentEntity Member { get; set; }
    }
}
