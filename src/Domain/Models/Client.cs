using System;

namespace Doctrina.Domain.Models
{
    public class Client
    {
        /// <summary>
        /// The primary key of the client
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Was created at
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Was last updated at
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Name of the client
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// API key for authorization
        /// </summary>
        public string API { get; set; }

        /// <summary>
        /// Wether client is enabled or not.
        /// </summary>
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
        /// The id of the <see cref="Store"/> that statements are stored in.
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// The store this client belongs to.
        /// </summary>
        public virtual Store Store { get; set; }

        /// <summary>
        /// The organisation this client belong to.
        /// </summary>
        public virtual Organisation Organisation { get; set; }
    }
}
