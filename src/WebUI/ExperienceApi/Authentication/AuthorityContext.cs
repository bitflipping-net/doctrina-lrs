using Doctrina.Application.Common.Interfaces;
using Doctrina.ExperienceApi.Data;

namespace Doctrina.WebUI.ExperienceApi.Authentication
{
    public class AuthorityContext : IAuthorityContext
    {
        public Agent Authority { get; set; }
    }
}