using System;

namespace Doctrina.Domain.Entities
{
    public class GroupMemberEntity
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public Guid GroupMemberId { get; set; }

        public Guid GroupId { get; set; }

        public Guid AgentId { get; set; }

        public virtual GroupEntity Group { get; set; }
        public virtual AgentEntity Agent { get; set; }
    }
}
