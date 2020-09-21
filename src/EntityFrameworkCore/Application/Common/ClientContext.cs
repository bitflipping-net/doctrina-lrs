using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Common.Models;
using Doctrina.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Common
{
    public class ClientContext : IClientContext
    {
        private readonly IDoctrinaDbContext _dbContext;
        private readonly IStoreContext _storeContext;
        private readonly IOrganisationContext _organisationContext;

        public ClientContext(IStoreContext storeContext, IOrganisationContext organisationContext, IDoctrinaDbContext dbContext)
        {
            _dbContext = dbContext;
            _storeContext = storeContext;
            _organisationContext = organisationContext;
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

            var client = await _dbContext.Clients.SingleOrDefaultAsync(x => x.API == authHeaderValue, cancellationToken);

            if (!client.Enabled)
            {
                return ClientAuthenticationResult.Fail("Unauthorized");
            }

            // Configure organization and store
            var store = await _storeContext.AuthorizeAsync(client, cancellationToken);
            await _organisationContext.SetupStoreAsync(client.Store, cancellationToken);

            return ClientAuthenticationResult.Success(client);
        }
    }
}
