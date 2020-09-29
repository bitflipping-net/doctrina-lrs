using System.Threading;
using System.Threading.Tasks;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Common.Models;
using Doctrina.Domain.Models;
using Doctrina.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Doctrina.Application.Common
{
    public class ClientHttpContext : IClientHttpContext
    {
        private readonly IDoctrinaDbContext _dbContext;
        private Client _client;

        public ClientHttpContext(IDoctrinaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Authenticate with auth header
        /// </summary>
        /// <param name="authHeaderValue"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="ClientAuthenticationResult"/></returns>
        public async Task<ClientAuthenticationResult> AuthenticateAsync(string authHeaderValue, CancellationToken cancellationToken = default)
        {
            if (authHeaderValue.StartsWith("basic"))
            {
                authHeaderValue = authHeaderValue.Substring("basic".Length).Trim();
            }

            var result = new ClientAuthenticationResult();

            Client client = await _dbContext.Clients.SingleOrDefaultAsync(x => x.API == authHeaderValue, cancellationToken);

            if (!client.Enabled)
            {
                return ClientAuthenticationResult.Fail("Unauthorized");
            }

            _client = client;

            return ClientAuthenticationResult.Success(client);
        }

        public Domain.Models.Client GetClient()
        {
            return _client;
        }
    }
}
