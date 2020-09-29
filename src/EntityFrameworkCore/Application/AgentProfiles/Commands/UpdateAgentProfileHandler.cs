using Doctrina.Application.Agents.Queries;
using Doctrina.Application.Common.Exceptions;
using Doctrina.Domain.Models.Documents;
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
        private readonly IStoreDbContext _context;

        public UpdateAgentProfileHandler(IStoreDbContext context)
        {
            _context = context;
        }

        public async Task<AgentProfileEntity> Handle(UpdateAgentProfileCommand request, CancellationToken cancellationToken)
        {
            var storeId = _context.StoreId;
            var profile = await _context.Documents
                            .OfType<AgentProfileEntity>()
                            .Where(x => x.PersonaId == request.Persona.PersonaId && x.StoreId == storeId)
                            .SingleOrDefaultAsync(x => x.ProfileId == request.ProfileId, cancellationToken);

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
