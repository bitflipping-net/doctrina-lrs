using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;

namespace Doctrina.WebUI.ExperienceApi.Authentication
{
    public class AuthorityContext : IAuthorityContext
    {
        public AgentEntity Authority { get; set; }
    }
}