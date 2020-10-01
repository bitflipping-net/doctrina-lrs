using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Documents;
using Doctrina.ExperienceApi.Data;
using MediatR;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class UpsertAgentProfileCommand : IRequest<AgentProfileModel>
    {
        public PersonaModel Persona { get; set; }
        public string ProfileId { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
