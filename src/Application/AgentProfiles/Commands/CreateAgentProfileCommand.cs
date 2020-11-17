using Doctrina.Domain.Entities.Documents;
using MediatR;
using System;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class CreateAgentProfileCommand : IRequest<AgentProfileEntity>
    {
        public Guid AgentId { get; private set; }
        public string ProfileId { get; private set; }
        public byte[] Content { get; private set; }
        public string ContentType { get; private set; }

        public static CreateAgentProfileCommand Create(Guid agentId, string profileId, byte[] content, string contentType)
        {
            return new CreateAgentProfileCommand()
            {
                AgentId = agentId,
                ProfileId = profileId,
                Content = content,
                ContentType = contentType
            };
        }
    }
}
