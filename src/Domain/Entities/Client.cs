using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public class Client : IClient
    {
        /// <summary>
        /// The primary key of the client
        /// </summary>
        public long ClientId { get; set; }

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
    }
}
