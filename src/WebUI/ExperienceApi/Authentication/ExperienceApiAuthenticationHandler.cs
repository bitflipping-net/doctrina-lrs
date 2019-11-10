using Doctrina.Application.Common.Interfaces;
using Doctrina.ExperienceApi.Data;
using Doctrina.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Authentication
{
    public class ExperienceApiAuthenticationHandler : AuthenticationHandler<ExperienceApiAuthenticationOptions>
    {
        private readonly DoctrinaAuthorizationDbContext _authorizationDbContext;
        private readonly IAuthorityContext _authority;
        private readonly IWebHostEnvironment _environment;

        public ExperienceApiAuthenticationHandler(
            IOptionsMonitor<ExperienceApiAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            DoctrinaAuthorizationDbContext authorizationDbContext,
            IAuthorityContext authority,
            IWebHostEnvironment environment
        ) : base(options, logger, encoder, clock)
        {
            _authorizationDbContext = authorizationDbContext;
            _authority = authority;
            _environment = environment;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Create authenticated user
            var identities = new List<ClaimsIdentity> { new ClaimsIdentity(AuthenticationTypes.Basic) };
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), ExperienceApiAuthenticationOptions.DefaultScheme);

            await Task.CompletedTask;

            _authority.Authority = new Agent()
            {
                Account = new Account()
                {
                    HomePage = new Uri($"{Request.Scheme}://{Request.Host}"),
                    Name = "TestClientApp" // TODO: Name of the client app authorized
                }
            };

            return AuthenticateResult.Success(ticket);
        }
    }
}