using Microsoft.AspNetCore.Authentication;

namespace Doctrina.WebUI.ExperienceApi.Authentication
{
    public class ExperienceApiAuthenticationOptions : AuthenticationSchemeOptions
    {
        public static string DefaultScheme { get; } = "Authority Scheme";

        public ExperienceApiAuthenticationOptions()
        {
        }

    }
}