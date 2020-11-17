using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class DeleteAgentProfileCommand : IRequest
    {
        public string ProfileId { get; private set; }

        public Guid AgentId { get; private set; }

        public static DeleteAgentProfileCommand Create(string profileId, Guid agent)
        {
            return new DeleteAgentProfileCommand()
            {
                ProfileId = profileId,
                AgentId = agent
            };
        }
    }
}
