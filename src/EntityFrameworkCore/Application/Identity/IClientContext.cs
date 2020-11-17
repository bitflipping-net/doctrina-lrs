using Doctrina.ExperienceApi.Data;
using System;

namespace Doctrina.Application.Identity
{
    /// <summary>
    /// Client Context API for information about current authorized xAPI Client.
    /// </summary>
    public interface IClientContext
    {
        Guid GetClientId();
        /// <summary>
        /// Returns JSON encoded representation of the client.
        /// </summary>
        Agent GetClientAuthority();
    }
}
