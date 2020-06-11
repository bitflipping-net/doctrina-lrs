using System.Collections.Generic;

namespace Doctrina.Domain.Entities.Interfaces
{
    public interface IGroupEntity
    {
        ICollection<GroupMemberEntity> Members { get; set; }
    }
}
