using Doctrina.Application.Common.Models;
using Doctrina.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Common
{
    public interface IClientHttpContext
    {
        Task<ClientAuthenticationResult> AuthenticateAsync(string authHeaderValue, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the current authorized client
        /// </summary>
        /// <returns>Client that has been authorized</returns>
        Client GetClient();
    }
}
