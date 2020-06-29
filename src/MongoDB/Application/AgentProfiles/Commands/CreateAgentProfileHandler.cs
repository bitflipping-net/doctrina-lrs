using System.Threading;
using System.Threading.Tasks;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class CreateAgentProfileHandler : IRequestHandler<CreateAgentProfileCommand, AgentProfileEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;

        public CreateAgentProfileHandler(IDoctrinaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator =mediator;
        }

        public async Task<AgentProfileEntity> Handle(CreateAgentProfileCommand request, CancellationToken cancellationToken)
        {
            var agent = await _mediator.Send(UpsertActorCommand.Create(request.Agent), cancellationToken);

            var profile = new AgentProfileEntity(request.Content, request.ContentType)
            {
                ProfileId = request.ProfileId,
                AgentId = agent.AgentId
            };

            _context.AgentProfiles.Add(profile);
            await _context.SaveChangesAsync(cancellationToken);

            return profile;
        }
    }
}