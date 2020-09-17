using Doctrina.Domain.Entities.Interfaces;
using System;

namespace Doctrina.Domain.Entities
{
    public class AgentEntity : IStatementObjectEntity, IAgentEntity
    {
        public virtual Entities.ObjectType ObjectType => Entities.ObjectType.Agent;

        /// <summary>
        /// The primary key
        /// </summary>
        public Guid AgentId { get; set; }

        /// <summary>
        /// The name of the agent
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PersonaIdentifier IFI { get; set; }
    }
}
