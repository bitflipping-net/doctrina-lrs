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

        public virtual EntityObjectType ObjectType { get; set; }

        public Guid AgentId { get; set; }

        public string Hash { get; set; }

        public string Name { get; set; }

        public string Mbox { get; set; }

        public string Mbox_SHA1SUM { get; set; }

        public string OpenId { get; set; }

        public Account Account { get; set; }
    }
}
