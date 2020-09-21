using Doctrina.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Common
{
    public interface IClientContext
    {
        Task<ClientAuthenticationResult> AuthenticateAsync(string authHeaderValue, CancellationToken cancellationToken = default);
    }
}
