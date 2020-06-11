using AutoMapper;
using Doctrina.Application.Activities.Queries;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityProfiles.Queries
{
    public class GetActivityProfileQueryHandler : IRequestHandler<GetActivityProfileQuery, ActivityProfileEntity>
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

        public async Task<ActivityProfileEntity> Handle(GetActivityProfileQuery request, CancellationToken cancellationToken)
        {
            var activityEntity = await _mediator.Send(GetActivityQuery.Create(request.ActivityId), cancellationToken);
            if(activityEntity == null)
            {
                return null;
            }

            return await _context.ActivityProfiles.GetProfileAsync(activityEntity.ActivityId, request.ProfileId, request.Registration, cancellationToken);
        }
    }
}
