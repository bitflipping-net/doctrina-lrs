using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.ActivityProfiles.Commands;
using Doctrina.Application.ActivityProfiles.Queries;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityProfiles
{
    public class ActivityProfilesHandler :
        IRequestHandler<CreateActivityProfileCommand, ActivityProfileDocument>,
        IRequestHandler<UpdateActivityProfileCommand>,
        IRequestHandler<GetActivityProfileQuery, ActivityProfileDocument>,
        IRequestHandler<GetActivityProfilesQuery, ICollection<ActivityProfileDocument>>,
        IRequestHandler<DeleteActivityProfileCommand>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ActivityProfilesHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateActivityProfileCommand request, CancellationToken cancellationToken)
        {
            ActivityProfileEntity profile = await GetProfile(request.ActivityId, request.ProfileId, request.Registration, cancellationToken);

            profile.Document.UpdateDocument(request.Content, request.ContentType);

            _context.ActivityProfiles.Update(profile);
            await _context.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }

        public async Task<ActivityProfileDocument> Handle(CreateActivityProfileCommand request, CancellationToken cancellationToken)
        {
            var activity = await _mediator.Send(new MergeActivityIriCommand()
            {
                ActivityId = request.ActivityId
            });

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

        public async Task<ActivityProfileDocument> Handle(GetActivityProfileQuery request, CancellationToken cancellationToken)
        {
            ActivityProfileEntity profile = await GetProfile(request.ActivityId, request.ProfileId, request.Registration, cancellationToken);

            return _mapper.Map<ActivityProfileDocument>(profile);
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

        public async Task<Unit> Handle(DeleteActivityProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await GetProfile(request.ActivityId, request.ProfileId, request.Registration, cancellationToken);

            _context.ActivityProfiles.Remove(profile);
            await _context.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }

        private async Task<ActivityProfileEntity> GetProfile(Iri activityId, string profileId, Guid? registration, CancellationToken cancellationToken)
        {
            string activityHash = activityId.ComputeHash();
            ActivityProfileEntity profile = await _context.ActivityProfiles.FirstOrDefaultAsync(x =>
                x.Activity.Hash == activityHash &&
                x.ProfileId == profileId &&
                x.RegistrationId == registration,
                cancellationToken);
            return profile;
        }
    }
}
