using System;

namespace Doctrina.Domain.Models
{
    /// <summary>
    /// A persona is an identify on a platform
    /// </summary>
    public class PersonaModel : IStoreOwnedEntity
    {
        /// <summary>
        /// The primary Key
        /// </summary>
        public Guid PersonaId { get; set; }

        public ObjectType ObjectType { get; set; }

        /// <summary>
        /// The name or alias of the persona
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The id of the <see cref="Store"/> this belongs to.
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// The <see cref="Models.Store"/> this identifier belongs to 
        /// </summary>
        public virtual Store Store { get; set; }

        /// <summary>
        /// The IFI type (account, mbox, mbox_sha1sum, or openid)
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The value of the IFI.
        /// </summary>
        public string Value { get; set; }
    }
}
