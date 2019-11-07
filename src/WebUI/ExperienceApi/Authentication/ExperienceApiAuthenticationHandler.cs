using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Doctrina.Application.Interfaces;
using Doctrina.ExperienceApi.Data;

namespace Doctrina.WebUI.ExperienceApi.Authentication
{
    public class ExperienceApiAuthenticationHandler : AuthenticationHandler<ExperienceApiAuthenticationOptions>
    {
        private readonly ICurrentAuthority _authority;

        public ExperienceApiAuthenticationHandler(
            IOptionsMonitor<ExperienceApiAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, ICurrentAuthority authority)
            : base(options, logger, encoder, clock)
        {
            _authority = authority;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Create authenticated user
            var identities = new List<ClaimsIdentity> { new ClaimsIdentity(AuthenticationTypes.Basic) };
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), ExperienceApiAuthenticationOptions.DefaultScheme);

            await Task.CompletedTask;

            _authority.Authority = new Agent()
            {

            };

            return AuthenticateResult.Success(ticket);
        }
    }
}