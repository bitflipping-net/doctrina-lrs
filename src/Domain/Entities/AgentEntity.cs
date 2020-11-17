using Doctrina.Domain.Entities.Interfaces;
using System;

namespace Doctrina.Domain.Entities
{
    public class AgentEntity : IStatementObjectEntity, IAgentEntity
    {
        public AgentEntity()
        {
            ObjectType = EntityObjectType.Agent;
        }

        public EntityObjectType ObjectType { get; set; }

        public Guid AgentId { get; set; }
        public Ifi IFI_Key { get; set; }
        public string IFI_Value { get; set; }

        public Guid? PersonId { get; set; }
        public virtual PersonEntity Person { get; set; }
    }
}
