using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;

namespace Doctrina.Application.AgentProfiles.Queries
{
    public class GetAgentProfileQuery : IRequest<AgentProfileDocument>
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
