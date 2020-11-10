using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Doctrina.Server.Authentication
{
    /// <summary>
    /// An AuthenticationHandler<TOptions> that can perform LRS Basic Client based authentication.
    /// </summary>
    public class LrsBasicClientHandler : AuthenticationHandler<ExperienceApiAuthenticationOptions>
    {
        private readonly IWebHostEnvironment _environment;

        public LrsBasicClientHandler(
            IOptionsMonitor<ExperienceApiAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Create authenticated user
            var identities = new List<ClaimsIdentity> { new ClaimsIdentity(AuthenticationTypes.Basic) };
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), ExperienceApiAuthenticationOptions.DefaultScheme);

            await Task.CompletedTask;

            //_authority.Authority = new Domain.Entities.AgentEntity()
            //{
            //    Account = new Account()
            //    {
            //        HomePage = $"{Request.Scheme}://{Request.Host}",
            //        Name = "TestClientApp" // TODO: Name of the client app authorized
            //    }
            //};

            return AuthenticateResult.Success(ticket);
        }
    }
}