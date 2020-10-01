using AutoMapper;
using Doctrina.Application.AgentProfiles.Queries;
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
    public class GetAgentProfileQueryHandler : IRequestHandler<GetAgentProfileQuery, AgentProfileModel>
    {
        private readonly IStoreDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAgentProfileQueryHandler(IStoreDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<AgentProfileModel> Handle(GetAgentProfileQuery request, CancellationToken cancellationToken)
        {
            var profile = await _context.Documents
                .OfType<AgentProfileModel>()
                .AsNoTracking()
                .Where(x => x.StoreId == _context.StoreId)
                .Where(x => x.PersonaId == request.Persona.PersonaId)
                .SingleOrDefaultAsync(x => x.ProfileId == request.ProfileId, cancellationToken);

            return profile;
        }
    }
}
