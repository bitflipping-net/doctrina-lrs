using AutoMapper;
using Doctrina.Application.AgentProfiles.Queries;
using Doctrina.Application.Agents.Queries;
using Doctrina.Domain.Entities.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.AgentProfiles
{
    public class GetAgentProfileQueryHandler : IRequestHandler<GetAgentProfileQuery, AgentProfileEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAgentProfileQueryHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<AgentProfileEntity> Handle(GetAgentProfileQuery request, CancellationToken cancellationToken)
        {
            var agent = await _mediator.Send(GetAgentQuery.Create(request.Agent));

            if (agent == null)
            {
                return null;
            }

            var profile = await _context.AgentProfiles
                .Include(x => x.Document)
                .AsNoTracking()
                .Where(x => x.AgentId == agent.AgentId)
                .SingleOrDefaultAsync(x => x.ProfileId == request.ProfileId, cancellationToken);

            return profile;
        }
    }
}