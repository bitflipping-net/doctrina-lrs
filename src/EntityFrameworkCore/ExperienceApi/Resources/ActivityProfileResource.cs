using AutoMapper;
using Doctrina.Application.ActivityProfiles.Commands;
using Doctrina.Application.ActivityProfiles.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.ExperienceApi.Server.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Resources
{
    public class ActivityProfileResource : IActivityProfileResource
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ActivityProfileResource(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IDocument> CreateProfile(Iri activityId, string profileId, byte[] content, string contentType, Guid? registration = null, CancellationToken cancellationToken = default)
        {
            var profile = await mediator.Send(new CreateActivityProfileCommand()
            {
                ProfileId = profileId,
                ActivityId = activityId,
                Content = content,
                ContentType = contentType,
                Registration = registration
            }, cancellationToken);

            return mapper.Map<IDocument>(profile);
        }

        /// <inheritdoc/>
        public Task DeleteActivityProfile(Iri activityId, string profileId, Guid? registration = null, CancellationToken cancellationToken = default)
        {
            return mediator.Send(DeleteActivityProfileCommand.Create(profileId, activityId, registration), cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IDocument> GetActivityProfile(Iri activityId, string profileId, Guid? registration = null, CancellationToken cancellationToken = default)
        {
            var profile = await mediator.Send(new GetActivityProfileQuery()
            {
                ProfileId = profileId,
                ActivityId = activityId,
                Registration = registration
            }, cancellationToken);

            return mapper.Map<IDocument>(profile);
        }

        /// <inheritdoc/>
        public async Task<ICollection<IDocument>> GetActivityProfiles(Iri activityId, DateTimeOffset? since = null, CancellationToken cancellationToken = default)
        {
            var documents = await mediator.Send(new GetActivityProfilesQuery()
            {
                ActivityId = activityId,
                Since = since
            }, cancellationToken);

            return mapper.Map<ICollection<IDocument>>(documents);
        }

        /// <inheritdoc/>
        public Task UpdateProfile(Iri activityId, string profileId, byte[] body, string contentType, Guid? registration = null, CancellationToken cancellationToken = default)
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
