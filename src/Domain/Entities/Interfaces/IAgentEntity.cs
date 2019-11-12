using System;

namespace Doctrina.Domain.Entities.Interfaces
{
    public interface IAgentEntity
    {
        Guid AgentId { get; set; }
        string Hash { get; set; }
        string Name { get; set; }
        string Mbox { get; set; }
        string Mbox_SHA1SUM { get; set; }
        string OpenId { get; set; }
        Account Account { get; set; }
    }
}
