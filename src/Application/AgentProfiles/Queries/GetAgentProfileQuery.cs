using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Documents;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.AgentProfiles.Queries
{
    public class GetAgentProfileQuery : IRequest<AgentProfileModel>
    {
        public PersonaModel Persona { get; set; }
        public string ProfileId { get; set; }

        public static GetAgentProfileQuery Create(PersonaModel persona, string profileId)
        {
            return new GetAgentProfileQuery()
            {
                Persona = persona,
                ProfileId = profileId
            };
        }
    }
}
