using System;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class DeleteAgentProfileCommand : IRequest
    {
        public string ProfileId { get; private set; }

        public Agent Agent { get; private set; }

        public static DeleteAgentProfileCommand Create(string profileId, Agent agent)
        {
            return new DeleteAgentProfileCommand()
            {
                ProfileId = profileId,
                Agent = agent
            };
        }
    }
}
