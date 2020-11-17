using Doctrina.Domain.Entities.Documents;
using MediatR;
using System;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class UpsertAgentProfileCommand : IRequest<AgentProfileEntity>
    {
        public Guid AgentId { get; set; }
        public string ProfileId { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
