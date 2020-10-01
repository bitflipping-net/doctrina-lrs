using Doctrina.Application.AgentProfiles.Commands;
using Doctrina.Application.Agents.Queries;
using Doctrina.Domain.Models.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.AgentProfiles
{
    public class DeleteAgentProfileHandler : IRequestHandler<DeleteAgentProfileCommand>
    {
        private readonly IStoreDbContext _context;
        private readonly IMediator _mediator;

        public DeleteAgentProfileHandler(IStoreDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteAgentProfileCommand request, CancellationToken cancellationToken)
        {
            AgentProfileModel profile = await _context.Documents
                            .OfType<AgentProfileModel>()
                            .AsNoTracking()
                            .Where(x=> x.StoreId == _context.StoreId)
                            .Where(x => x.PersonaId == request.Persona.PersonaId)
                            .SingleOrDefaultAsync(x => x.ProfileId == request.ProfileId, cancellationToken);

            if (profile != null)
            {
                _context.Documents.Remove(profile);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return await Unit.Task;
        }
    }
}
