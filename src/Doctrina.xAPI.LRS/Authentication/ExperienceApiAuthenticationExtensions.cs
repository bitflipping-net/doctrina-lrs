using System;
using Microsoft.AspNetCore.Authentication;

namespace Doctrina.xAPI.Store.Authentication
{
    public static class ExperienceApiAuthenticationExtensions
    {
        public static AuthenticationBuilder AddExperienceApiAuthentication(this AuthenticationBuilder builder, Action<ExperienceApiAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<ExperienceApiAuthenticationOptions, ExperienceApiAuthenticationHandler>(ExperienceApiAuthenticationOptions.DefaultScheme, "Experience API Authority", configureOptions);
        }
    }
}