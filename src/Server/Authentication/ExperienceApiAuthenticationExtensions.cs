using Microsoft.AspNetCore.Authentication;
using System;

namespace Doctrina.Server.Authentication
{
    public static class ExperienceApiAuthenticationExtensions
    {
        public static AuthenticationBuilder AddExperienceApiAuthentication(this AuthenticationBuilder builder)
        {
            return builder.AddExperienceApiAuthentication(options => { });
        }

        public static AuthenticationBuilder AddExperienceApiAuthentication(this AuthenticationBuilder builder, Action<ExperienceApiAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<ExperienceApiAuthenticationOptions, LrsBasicClientHandler>(ExperienceApiAuthenticationOptions.DefaultScheme, "Experience API Authority", configureOptions);
        }
    }
}