﻿using AutoMapper;
using Doctrina.Application.Activities.Queries;
using Doctrina.Domain.Entities.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System.Linq;
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
            if (activityEntity == null)
            {
                return null;
            }

            return await _context.Documents
                .OfType<ActivityProfileEntity>()
                .GetProfileAsync(activityEntity.ActivityId, request.ProfileId, request.Registration, cancellationToken);
        }
    }
}
