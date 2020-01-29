using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityProfiles.Commands
{
    public class CreateActivityProfileHandler : IRequestHandler<CreateActivityProfileCommand, ActivityProfileDocument>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateActivityProfileHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ActivityProfileDocument> Handle(CreateActivityProfileCommand request, CancellationToken cancellationToken)
        {
            var activity = (ActivityEntity)await _mediator.Send(MergeActivityCommand.Create(request.ActivityId));

            var profile = new ActivityProfileEntity(request.Content, request.ContentType)
            {
                ProfileId = request.ProfileId,
                Activity = activity,
                RegistrationId = request.Registration
            };

            _context.ActivityProfiles.Add(profile);
            await _context.SaveChangesAsync(cancellationToken);

            //return profile;
            return _mapper.Map<ActivityProfileDocument>(profile);
        }
    }
}
