using Doctrina.Domain.Entities.Documents;
using MediatR;
using System;

namespace Doctrina.Application.AgentProfiles.Queries
{
    public class GetAgentProfileQuery : IRequest<AgentProfileEntity>
    {
        public Guid AgentId { get; set; }
        public string ProfileId { get; set; }

        public static GetAgentProfileQuery Create(Guid agentId, string profileId)
        {
            return new GetAgentProfileQuery()
            {
                AgentId = agentId,
                ProfileId = profileId
            };
        }
    }
}
