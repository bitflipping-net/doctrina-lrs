using System;

namespace Doctrina.Domain.Entities
{
    public class GroupMemberEntity
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public Guid GroupMemberId { get; set; }

        /// <summary>
        /// The Id of the Group
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// The Id of the Agent
        /// </summary>
        public Guid AgentId { get; set; }

        public virtual GroupEntity Group { get; set; }
        public virtual AgentEntity Agent { get; set; }
    }
}
