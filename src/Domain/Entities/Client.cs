using System;

namespace Doctrina.Domain.Entities
{
    public class Client
    {
        public Guid ClientId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Name { get; set; }

        public string API { get; set; }

        public bool Enabled { get; set; }

        /// <summary>
        /// A JSON encoded string of an agent to be set on statements that the client pushes.
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// An array of permission scopes
        /// </summary>
        public string[] Scopes { get; set; }

        /// <summary>
        /// The organisation this client belong to.
        /// </summary>
        public Guid OrganisationId { get; set; }

        /// <summary>
        /// The id of the <see cref="Store"/> that pushed statements are stored in.
        /// </summary>
        public Guid StoreId { get; set; }

        public virtual Store Store { get; set; }
    }
}
