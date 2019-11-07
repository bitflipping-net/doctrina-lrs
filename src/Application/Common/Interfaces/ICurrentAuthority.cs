using Doctrina.ExperienceApi.Data;

namespace Doctrina.Application.Common.Interfaces
{
    public interface ICurrentAuthority
    {
        Agent Authority { get; set; }
    }
}