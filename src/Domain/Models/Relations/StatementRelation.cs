using System;

namespace Doctrina.Domain.Models.Relations
{
    public class StatementRelation : IRelation, IStoreOwnedEntity
    {
        /// <summary>
        /// The id of the <see cref="Parent"/> entity.
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// The unique key for this relationship. 
        /// </summary>
        public ObjectType ObjectType { get; set; }

        /// <summary>
        /// The id of the <see cref="Child"/> entity.
        /// </summary>
        public Guid ChildId { get; set; }

        /// <summary>
        /// The id the <see cref="Store"/> this relationship belongs to
        /// </summary>
        public Guid StoreId { get; set; }
    }
}
