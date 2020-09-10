using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.AgentProfiles.Queries
{
    public class GetAgentProfileQuery : IRequest<AgentProfileEntity>
    {
        public Agent Agent { get; set; }
        public string ProfileId { get; set; }

        public static GetAgentProfileQuery Create(Agent agent, string profileId)
        {
            return new GetAgentProfileQuery()
            {
                Agent = agent,
                ProfileId = profileId
            };
        }
    }
}
