using AutoMapper;
using Doctrina.Application.Activities.Queries;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityProfiles.Queries
{
    public class GetActivityProfilesHandler : IRequestHandler<GetActivityProfilesQuery, ICollection<ActivityProfileEntity>>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetActivityProfilesHandler(IDoctrinaDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ICollection<ActivityProfileEntity>> Handle(GetActivityProfilesQuery request, CancellationToken cancellationToken)
        {
            var activity = await _mediator.Send(GetActivityQuery.Create(request.ActivityId), cancellationToken);

            var query = _context.Documents
                .OfType<ActivityProfileEntity>()
                .Where(x => x.ActivityId == activity.ActivityId);
            if (request.Since.HasValue)
            {
                query = query.Where(x => x.UpdatedAt >= request.Since);
            }
            query = query.OrderByDescending(x => x.UpdatedAt);
            return await query.ToListAsync(cancellationToken);
        }
    }
}
