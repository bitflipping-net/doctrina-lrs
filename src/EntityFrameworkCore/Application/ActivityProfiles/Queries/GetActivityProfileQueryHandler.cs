using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Doctrina.Application.Activities.Queries;
using Doctrina.Domain.Models.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;

namespace Doctrina.Application.ActivityProfiles.Queries
{
    public class GetActivityProfileQueryHandler : IRequestHandler<GetActivityProfileQuery, ActivityProfileModel>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetActivityProfileQueryHandler(IDoctrinaDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActivityProfileModel> Handle(GetActivityProfileQuery request, CancellationToken cancellationToken)
        {
            var activityEntity = await _mediator.Send(GetActivityQuery.Create(request.ActivityId), cancellationToken);
            if (activityEntity == null)
            {
                return null;
            }

            var profile = await _context.ActivityProfiles.GetProfileAsync(activityEntity.ActivityId, request.ProfileId, request.Registration, cancellationToken);

            return profile;
        }
    }
}
