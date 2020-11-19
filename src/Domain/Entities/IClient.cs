using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public interface IClient
    {
        Guid ClientId { get; set; }

        /// <summary>
        /// The api
        /// </summary>
        string API { get; set; }

        /// <summary>
        /// The xAPI Agent authority
        /// </summary>
        string Authority { get; set; }

        /// <summary>
        /// Then the client was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Wether the client is enabled.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// The name of the client
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The scopes this client is restricted by.
        /// </summary>
        List<string> Scopes { get; set; }

        /// <summary>
        /// When the client was last changed.
        /// </summary>
        DateTimeOffset UpdatedAt { get; set; }
    }
}