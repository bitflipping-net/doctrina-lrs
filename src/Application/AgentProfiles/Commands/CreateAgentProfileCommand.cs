using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Documents;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class CreateAgentProfileCommand : IRequest<AgentProfileEntity>
    {
        public Persona Persona { get; private set; }
        public string ProfileId { get; private set; }
        public byte[] Content { get; private set; }
        public string ContentType { get; private set; }

        public static CreateAgentProfileCommand Create(Persona persona, string profileId, byte[] content, string contentType)
        {
            return new CreateAgentProfileCommand()
            {
                Persona = persona,
                ProfileId = profileId,
                Content = content,
                ContentType = contentType
            };
        }
    }
}
