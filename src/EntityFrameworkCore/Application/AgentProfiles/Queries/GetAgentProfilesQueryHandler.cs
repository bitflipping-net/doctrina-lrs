using AutoMapper;
using Doctrina.Application.AgentProfiles.Queries;
using Doctrina.Application.Agents.Queries;
using Doctrina.Application.Personas.Queries;
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
        IRequestHandler<GetAgentProfilesQuery, ICollection<AgentProfileModel>>
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

        public async Task<ICollection<AgentProfileModel>> Handle(GetAgentProfilesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.AgentProfiles
                .AsNoTracking()
                .Where(a => a.PersonaId == request.Persona.PersonaId);

            if (request.Since.HasValue)
            {
                query = query.Where(x => x.UpdatedAt >= request.Since.Value);
            }

            query = query.OrderByDescending(x => x.UpdatedAt);

            return await query.ToListAsync(cancellationToken);
        }
    }
}
