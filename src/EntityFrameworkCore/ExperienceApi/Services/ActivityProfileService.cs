using AutoMapper;
using Doctrina.Application.ActivityProfiles.Commands;
using Doctrina.Application.ActivityProfiles.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.ExperienceApi.Server.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Services
{
    public class ActivityProfileService : IActivityProfileService
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ActivityProfileService(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<ActivityProfileDocument> CreateProfile(string profileId, Iri activityId, byte[] content, string contentType, Guid? registration, CancellationToken cancellationToken)
        {
            var profile = await mediator.Send(new CreateActivityProfileCommand()
            {
                ProfileId = profileId,
                ActivityId = activityId,
                Content = content,
                ContentType = contentType,
                Registration = registration
            }, cancellationToken);

            return mapper.Map<ActivityProfileDocument>(profile);
        }

        /// <inheritdoc/>
        public Task DeleteActivityProfile(string profileId, Iri activityId, Guid? registration, CancellationToken cancellationToken)
        {
            return mediator.Send(DeleteActivityProfileCommand.Create(profileId, activityId, registration), cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<ActivityProfileDocument> GetActivityProfile(string profileId, Iri activityId, Guid? registration, CancellationToken cancellationToken)
        {
            var profile = await mediator.Send(new GetActivityProfileQuery()
            {
                ProfileId = profileId,
                ActivityId = activityId,
                Registration = registration
            }, cancellationToken);

            return mapper.Map<ActivityProfileDocument>(profile);
        }

        /// <inheritdoc/>
        public async Task<ICollection<ActivityProfileDocument>> GetActivityProfiles(Iri activityId, DateTimeOffset? since, CancellationToken cancellationToken)
        {
            var documents = await mediator.Send(new GetActivityProfilesQuery()
            {
                ActivityId = activityId,
                Since = since
            }, cancellationToken);

            return mapper.Map<ICollection<ActivityProfileDocument>>(documents);
        }

        /// <inheritdoc/>
        public Task UpdateProfile(string profileId, Iri activityId, byte[] body, string contentType, Guid? registration, CancellationToken cancellationToken)
        {
            return mediator.Send(new UpdateActivityProfileCommand()
            {
                ProfileId = profileId,
                ActivityId = activityId,
                Content = body,
                ContentType = contentType
            }, cancellationToken);
        }
    }
}
