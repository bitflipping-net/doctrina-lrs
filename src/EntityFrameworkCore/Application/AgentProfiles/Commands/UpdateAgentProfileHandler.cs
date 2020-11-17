using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Server.Exceptions;
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
            var profile = await _context.Documents
                .OfType<AgentProfileEntity>()
                .Where(x => x.AgentId == request.AgentId)
                .SingleOrDefaultAsync(x => x.Key == request.ProfileId, cancellationToken);

            if (profile == null)
            {
                throw new NotFoundException("AgentProfile", request.ProfileId);
            }

            profile.UpdateDocument(request.Content, request.ContentType);

            _context.Documents.Update(profile);

            await _context.SaveChangesAsync(cancellationToken);

            return profile;
        }
    }
}