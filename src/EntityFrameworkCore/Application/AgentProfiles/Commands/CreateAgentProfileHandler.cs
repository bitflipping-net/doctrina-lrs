using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Personas.Commands;
using Doctrina.Domain.Models.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class CreateAgentProfileHandler : IRequestHandler<CreateAgentProfileCommand, AgentProfileEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;

        public CreateAgentProfileHandler(IDoctrinaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<AgentProfileEntity> Handle(CreateAgentProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = new AgentProfileEntity(request.Content, request.ContentType)
            {
                ProfileId = request.ProfileId,
                PersonaId = request.Persona.PersonaId
            };

            _context.AgentProfiles.Add(profile);
            await _context.SaveChangesAsync(cancellationToken);

            return profile;
        }
    }
}
