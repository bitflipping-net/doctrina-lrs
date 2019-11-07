using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class UpdateAgentProfileCommand : IRequest<AgentProfileDocument>
    {
        public Agent Agent { get; set; }
        public string ProfileId { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }

        public static UpdateAgentProfileCommand Create(Agent agent, string profileId, byte[] content, string contentType)
        {
            var cmd = new UpdateAgentProfileCommand()
            {
                Agent = agent,
                ProfileId = profileId,
                Content = content,
                ContentType = contentType
            };

            return cmd;
        }
    }
}
