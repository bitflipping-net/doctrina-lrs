using Doctrina.ExperienceApi.Data;

namespace Doctrina.Application.Interfaces
{
    public interface ICurrentAuthority
    {
        Agent Authority { get; set; }
    }
}