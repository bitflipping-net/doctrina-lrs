using System;

namespace Doctrina.Domain.Entities.Interfaces
{
    public interface IAgentEntity
    {
        Guid AgentId { get; set; }
        string Name { get; set; }
       Ifi IFI_Key {get;set;}
       string IFI_Value {get;set;}
    }
}
