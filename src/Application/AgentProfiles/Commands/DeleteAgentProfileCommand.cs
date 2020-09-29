using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class DeleteAgentProfileCommand : IRequest
    {
        public string ProfileId { get; private set; }

        public Persona Persona { get; private set; }

        public static DeleteAgentProfileCommand Create(string profileId, Persona persona)
        {
            return new DeleteAgentProfileCommand()
            {
                ProfileId = profileId,
                Persona = persona
            };
        }
    }
}
