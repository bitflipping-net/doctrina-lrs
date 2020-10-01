using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Documents;
using MediatR;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class UpdateAgentProfileCommand : IRequest<AgentProfileModel>
    {
        public PersonaModel Persona { get; set; }
        public string ProfileId { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }

        public static UpdateAgentProfileCommand Create(PersonaModel persona, string profileId, byte[] content, string contentType)
        {
            var cmd = new UpdateAgentProfileCommand()
            {
                Persona = persona,
                ProfileId = profileId,
                Content = content,
                ContentType = contentType
            };

            return cmd;
        }
    }
}
