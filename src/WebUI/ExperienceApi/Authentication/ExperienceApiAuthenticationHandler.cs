using Doctrina.Application.Common;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Authentication
{
    public class ExperienceApiAuthenticationHandler : AuthenticationHandler<ExperienceApiAuthenticationOptions>
    {
        private const string AUTHORIZATION = "Authorization";
        private readonly DoctrinaAuthorizationDbContext _authorizationDbContext;
        private readonly IClientContext _clientContext;
        private readonly IWebHostEnvironment _environment;
        public readonly IHttpContextAccessor _httpContextAccessor;

        public ExperienceApiAuthenticationHandler(
            IOptionsMonitor<ExperienceApiAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            DoctrinaAuthorizationDbContext authorizationDbContext,
            IClientContext clientContext,
            IWebHostEnvironment environment,
            IHttpContextAccessor httpContextAccessor
        ) : base(options, logger, encoder, clock)
        {
            _authorizationDbContext = authorizationDbContext;
            _clientContext = clientContext;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Unauthorized");

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.NoResult();
            }

            if(!authorizationHeader.StartsWith("Basic", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string basicAuth = authorizationHeader.Substring("basic".Length).Trim();

            if (string.IsNullOrEmpty(basicAuth))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var result = await _clientContext.AuthenticateAsync(basicAuth);

            if (result.IsSuccess)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.Client.Name),
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new System.Security.Principal.GenericPrincipal(identity, null);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail(result.Message);
        }
    }
}