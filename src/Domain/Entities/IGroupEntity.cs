using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public interface IGroupEntity
    {
        ICollection<AgentEntity> Members { get; set; }
    }
}
