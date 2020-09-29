using System;

namespace Doctrina.Domain.Models.Relations
{
    public interface IRelation
    {
        Guid ParentId { get; set; }

        ObjectType ObjectType { get; set; }

        Guid ChildId { get; set; }
    }
}
