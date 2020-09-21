using System;

namespace Doctrina.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupMemberIdentifier
    {
        /// <summary>
        /// The Id of the Group
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// The Id of the Agent
        /// </summary>
        public Guid PersonaIdentifierId { get; set; }

        public virtual GroupPersona Group { get; set; }
        public virtual Persona PersonaIdentifier { get; set; }
    }
}
