using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Doctrina.Domain.Entities
{
    public class ContextActivitiesEntity
    {
        public ContextActivitiesEntity()
        {
            Parent = new ContextActivityCollection();

            Grouping = new ContextActivityCollection();

            Category = new ContextActivityCollection();

            Other = new ContextActivityCollection();
        }

        public Guid ContextActivitiesId { get; set; }

        public ContextActivityCollection Parent { get; set; }

        public ContextActivityCollection Grouping { get; set; }

        public ContextActivityCollection Category { get; set; }

        public ContextActivityCollection Other { get; set; }
    }

    public class ContextActivityTypeEntity
    {
        /// <summary>
        /// Activity IRL ID
        /// </summary>
        public string Id { get; set; }
    }

    public class ContextActivityCollection : KeyedCollection<string, ContextActivityTypeEntity>
    {
        protected override string GetKeyForItem(ContextActivityTypeEntity item)
        {
            return item.Id;
        }
    }
}
