using AutoMapper;
using Doctrina.Application.Activities.Queries;
using Doctrina.Application.ActivityProfiles.Commands;
using Doctrina.Application.ActivityProfiles.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.ExperienceApi.Server.Models;
using Doctrina.ExperienceApi.Server.Resources;
using MediatR;
using System;
using System.Linq;
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
        public async Task<MultipleDocumentResult> GetActivityProfiles(Iri activityId, DateTimeOffset? since = null, CancellationToken cancellationToken = default)
        {
            var activity = await mediator.Send(GetActivityQuery.Create(activityId), cancellationToken);

            if(activity == null)
                return MultipleDocumentResult.Empty();

            var documents = await mediator.Send(new GetActivityProfilesQuery()
            {
                ActivityId = activityId,
                Since = since
            }, cancellationToken);

            if (!documents.Any())
                return MultipleDocumentResult.Empty();

            var ids = documents.Select(x => x.Key).ToHashSet();
            var lastModified = documents
                .OrderByDescending(x => x.UpdatedAt)
                .Select(x => x.UpdatedAt)
                .FirstOrDefault();

            return MultipleDocumentResult.Success(ids, lastModified);
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
