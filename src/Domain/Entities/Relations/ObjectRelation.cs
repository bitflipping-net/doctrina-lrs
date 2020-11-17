using Doctrina.Domain.Entities;
using System;

namespace Doctrina.Persistence.Configurations.Relations
{
    public class ObjectRelation
    {
        public EntityObjectType ParentObjectType { get; set; }
        public Guid ParentId { get; set; }

        public EntityObjectType ChildObjectType { get; set; }
        public Guid ChildId { get; set; }
    }
}
