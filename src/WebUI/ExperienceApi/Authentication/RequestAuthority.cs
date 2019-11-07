using Doctrina.Application.Interfaces;
using Doctrina.ExperienceApi.Data;

namespace Doctrina.WebUI.ExperienceApi.Authentication
{
    public class RequestAuthority : ICurrentAuthority
    {
        public Agent Authority { get; set; }
    }
}