using System.Collections.Generic;

namespace Doctrina.Domain.Entities.Interfaces
{
    public interface IGroupEntity
    {
        ICollection<AgentEntity> Members { get; set; }
    }
}
