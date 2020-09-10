﻿using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class CreateAgentProfileCommand : IRequest<AgentProfileEntity>
    {
        public Agent Agent { get; private set; }
        public string ProfileId { get; private set; }
        public byte[] Content { get; private set; }
        public string ContentType { get; private set; }

        public static CreateAgentProfileCommand Create(Agent agent, string profileId, byte[] content, string contentType)
        {
            return new CreateAgentProfileCommand()
            {
                Agent = agent,
                ProfileId = profileId,
                Content = content,
                ContentType = contentType
            };
        }
    }
}
