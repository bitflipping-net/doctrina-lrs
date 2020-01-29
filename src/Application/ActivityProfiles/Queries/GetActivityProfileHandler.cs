using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityProfiles.Queries
{
    public class GetActivityProfileHandler : IRequestHandler<GetActivityProfileQuery, ActivityProfileDocument>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;

        public GetActivityProfileHandler(IDoctrinaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActivityProfileDocument> Handle(GetActivityProfileQuery request, CancellationToken cancellationToken)
        {
            ActivityProfileEntity profile = await _context.ActivityProfiles.GetProfileAsync(request.ActivityId, request.ProfileId, request.Registration, cancellationToken);

            return _mapper.Map<ActivityProfileDocument>(profile);
        }
    }
}
