using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public class Client : IClient
    {
        public Client()
        {
            UpdatedAt = DateTimeOffset.UtcNow;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        /// <inheritdoc />
        public Guid ClientId { get; set; }

       /// <inheritdoc />
        public DateTimeOffset CreatedAt { get; set; }

        /// <inheritdoc />
        public DateTimeOffset UpdatedAt { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string API { get; set; }

        /// <inheritdoc />
        public bool Enabled { get; set; }

        /// <inheritdoc />
        public string Authority { get; set; }

        /// <inheritdoc />
        public List<string> Scopes { get; set; }
    }
}
