using Doctrina.Domain.Entities.Interfaces;
using System;

namespace Doctrina.Domain.Entities
{
    public class AgentEntity : IStatementObjectEntity, IAgentEntity
    {
        public virtual Entities.ObjectType ObjectType => Entities.ObjectType.Agent;

        /// <summary>
        /// The primary key of the agent of group
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the agent
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The persona of the agent (required)
        /// </summary>
        public Persona Persona { get; set; }

        public Guid StoreId { get; set; }
    }
}
