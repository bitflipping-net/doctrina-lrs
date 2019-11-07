using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Doctrina.Infrastructure.Identity
{
    public class DoctrinaAuthorizationDbContext : ApiAuthorizationDbContext<DoctrinaUser>
    {
        public DoctrinaAuthorizationDbContext(
            DbContextOptions<DoctrinaAuthorizationDbContext> options, 
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}
