using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class DeleteAgentProfileCommand : IRequest
    {
        public string ProfileId { get; set; }
        public Agent Agent { get; set; }
    }
}
