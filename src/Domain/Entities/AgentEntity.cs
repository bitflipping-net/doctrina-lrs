using System;

namespace Doctrina.Domain.Entities
{
    public class AgentEntity : IStatementObjectEntity, IAgentEntity
    {
        public virtual EntityObjectType ObjectType { get; set; } = EntityObjectType.Agent;

        public Guid AgentId { get; set; }

        public string Hash { get; set; }

        public string Name { get; set; }

        public string Mbox { get; set; }

        public string Mbox_SHA1SUM { get; set; }

        public string OpenId { get; set; }

        public Account Account { get; set; }
    }
}
