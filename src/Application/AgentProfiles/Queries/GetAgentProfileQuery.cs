using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Documents;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.AgentProfiles.Queries
{
    public class GetAgentProfileQuery : IRequest<AgentProfileEntity>
    {
        public Persona Persona { get; set; }
        public string ProfileId { get; set; }

        public static GetAgentProfileQuery Create(Persona persona, string profileId)
        {
            return new GetAgentProfileQuery()
            {
                Persona = persona,
                ProfileId = profileId
            };
        }
    }
}
