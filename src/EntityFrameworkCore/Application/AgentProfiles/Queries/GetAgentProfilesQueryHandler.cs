using AutoMapper;
using Doctrina.Application.AgentProfiles.Queries;
using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Documents;
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
            var agentEntity = _mapper.Map<AgentEntity>(request.Persona);
            var query = _context.AgentProfiles
                .AsNoTracking()
                .Include(x => x.Document)
                .Where(a => a.Agent.AgentId == agentEntity.Id);

            if (request.Since.HasValue)
            {
                query = query.Where(x => x.Document.LastModified >= request.Since.Value);
            }

            query = query.OrderByDescending(x => x.Document.LastModified);

            return await query.ToListAsync(cancellationToken);
        }
    }
}
