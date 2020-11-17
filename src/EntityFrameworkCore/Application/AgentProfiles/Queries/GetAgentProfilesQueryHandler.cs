using AutoMapper;
using Doctrina.Application.AgentProfiles.Queries;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.AgentProfiles
{
    public class GetAgentProfilesQueryHandler :
        IRequestHandler<GetAgentProfilesQuery, ICollection<AgentProfileEntity>>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAgentProfilesQueryHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ICollection<AgentProfileEntity>> Handle(GetAgentProfilesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Documents
                .AsNoTracking()
                .OfType<AgentProfileEntity>()
                .Where(a => a.Agent.AgentId == request.AgentId);

            if (request.Since.HasValue)
            {
                query = query.Where(x => x.UpdatedAt >= request.Since.Value);
            }

            query = query.OrderByDescending(x => x.UpdatedAt);

            return await query.ToListAsync(cancellationToken);
        }
    }
}
