using System;

namespace Doctrina.Domain.Entities
{
    public class Persona
    {
        /// <summary>
        /// The primary Key
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The id of the <see cref="Person"/> this belongs to, or null.
        /// </summary>
        public Guid? PersonId { get; set; }

        /// <summary>
        /// The person this persona belongs to, or null if not linked.
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// The id of the <see cref="Store"/> this belongs to.
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// The <see cref="Entities.Store"/> this identifier belongs to 
        /// </summary>
        public virtual Store Store { get; set; }

        /// <summary>
        /// The IFI type (account, mbox, mbox_sha1sum, or openid)
        /// </summary>
        public IFIType Type { get; set; }

        /// <summary>
        /// The value of the IFI.
        /// </summary>
        public string Value { get; set; }
    }
}
