using Doctrina.Domain.Entities;

namespace Doctrina.Application.Common.Interfaces
{
    public interface IAuthorityContext
    {
        AgentEntity Authority { get; set; }
    }
}