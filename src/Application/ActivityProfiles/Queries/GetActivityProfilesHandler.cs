using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityProfiles.Queries
{
    public class GetActivityProfilesHandler : IRequestHandler<GetActivityProfilesQuery, ICollection<ActivityProfileDocument>>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;

        public GetActivityProfilesHandler(IDoctrinaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<ActivityProfileDocument>> Handle(GetActivityProfilesQuery request, CancellationToken cancellationToken)
        {
            var activityHash = request.ActivityId.ComputeHash();
            var query = _context.ActivityProfiles.Where(x => x.Activity.Hash == activityHash);
            if (request.Since.HasValue)
            {
                query = query.Where(x => x.Document.LastModified >= request.Since);
            }
            query = query.OrderByDescending(x => x.Document.LastModified);
            return _mapper.Map<ICollection<ActivityProfileDocument>>(await query.ToListAsync(cancellationToken));
        }
    }
}
