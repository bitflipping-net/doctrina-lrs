using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;

namespace Doctrina.Application.Common.Interfaces
{
    public interface IAuthorityContext
    {
        AgentEntity Authority { get; set; }
    }
}