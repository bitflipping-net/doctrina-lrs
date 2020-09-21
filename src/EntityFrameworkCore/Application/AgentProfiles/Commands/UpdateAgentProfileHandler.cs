using Doctrina.Application.Agents.Queries;
using Doctrina.Application.Common.Exceptions;
using Doctrina.Domain.Entities.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class UpdateAgentProfileHandler : IRequestHandler<UpdateAgentProfileCommand, AgentProfileEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;

        public UpdateAgentProfileHandler(IDoctrinaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<AgentProfileEntity> Handle(UpdateAgentProfileCommand request, CancellationToken cancellationToken)
        {
            var agent = await _mediator.Send(GetAgentQuery.Create(request.Agent));

            var profile = await _context.AgentProfiles
                            .Include(x => x.Document)
                            .Where(x => x.AgentId == agent.Id)
                            .SingleOrDefaultAsync(x => x.ProfileId == request.ProfileId, cancellationToken);

            if (profile == null)
            {
                throw new NotFoundException("AgentProfile", request.ProfileId);
            }

            profile.Document.UpdateDocument(request.Content, request.ContentType);

            _context.AgentProfiles.Update(profile);
            await _context.SaveChangesAsync(cancellationToken);

            return profile;
        }
    }
}