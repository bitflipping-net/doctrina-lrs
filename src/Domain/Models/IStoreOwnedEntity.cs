using System;

namespace Doctrina.Domain.Models
{
    public interface IStoreOwnedEntity
    {
        /// <summary>
        /// The unique id of the store, this entity belongs to.
        /// </summary>
        Guid StoreId { get; set; }
    }
}
