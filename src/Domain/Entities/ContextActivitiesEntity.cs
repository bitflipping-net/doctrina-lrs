using Doctrina.Domain.Entities.OwnedTypes;
using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public class ContextActivitiesEntity
    {
        public ContextActivitiesEntity()
        {
            Parent = new HashSet<ContextActivityEntity>();

            Grouping = new HashSet<ContextActivityEntity>();

            Category = new HashSet<ContextActivityEntity>();

            Other = new HashSet<ContextActivityEntity>();
        }

        public Guid ContextActivitiesId { get; set; }

        public ICollection<ContextActivityEntity> Parent { get; set; }

        public ICollection<ContextActivityEntity> Grouping { get; set; }

        public ICollection<ContextActivityEntity> Category { get; set; }

        public ICollection<ContextActivityEntity> Other { get; set; }
    }
}
