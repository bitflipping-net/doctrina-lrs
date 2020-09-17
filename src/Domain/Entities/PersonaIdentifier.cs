using System;

namespace Doctrina.Domain.Entities
{
    public class PersonaIdentifier
    {
        /// <summary>
        /// The primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The id of the <see cref="Persona"/> this belongs to.
        /// </summary>
        public Guid PersonaId { get; set; }

        /// <summary>
        /// The id of the <see cref="Organisation"/> this belongs to
        /// </summary>
        public Guid OrganisationId { get; set; }

        /// <summary>
        /// The IFI type (account, mbox, mbox_sha1sum, or openid)
        /// </summary>
        public IFIType Key { get; set; }

        /// <summary>
        /// The value of the IFI.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The persona this IFI belongs to
        /// </summary>
        public virtual Persona Persona { get; set; }
    }
}
