using AutoMapper;
using Doctrina.Application.AgentProfiles.Commands;
using Doctrina.Application.AgentProfiles.Queries;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Agents.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.ExperienceApi.Server.Exceptions;
using Doctrina.ExperienceApi.Server.Models;
using Doctrina.ExperienceApi.Server.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Resources
{
    public class AgentProfileResource : IAgentProfileResource
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public AgentProfileResource(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<IDocument> CreateAgentProfile(Agent agent, string profileId, byte[] content, string contentType, CancellationToken cancellationToken = default)
        {
            var actor = await mediator.Send(UpsertActorCommand.Create(agent), cancellationToken);

            var profile = await mediator.Send(CreateAgentProfileCommand.Create(actor.AgentId, profileId, content, contentType), cancellationToken);

            return mapper.Map<IDocument>(profile);
        }

        public async Task DeleteAgentProfile(Agent agent, string profileId, CancellationToken cancellationToken = default)
        {
            var agentEntity = await mediator.Send(GetAgentQuery.Create(agent), cancellationToken);

            if (agentEntity == null)
                return;

            await mediator.Send(DeleteAgentProfileCommand.Create(profileId, agentEntity.AgentId), cancellationToken);
        }

        public async Task<IDocument> GetAgentProfile(Agent agent, string profileId, CancellationToken cancellationToken = default)
        {
            var agentEntity = await mediator.Send(GetAgentQuery.Create(agent), cancellationToken);

            if (agentEntity == null)
                return null;

            var profile = await mediator.Send(GetAgentProfileQuery.Create(agentEntity.AgentId, profileId));

            return mapper.Map<IDocument>(profile);
        }

        public async Task<MultipleDocumentResult> GetAgentProfiles(Agent agent, DateTimeOffset? since = null, CancellationToken cancellationToken = default)
        {
            var agentEntity = await mediator.Send(GetAgentQuery.Create(agent), cancellationToken);

            if (agentEntity == null)
                return MultipleDocumentResult.Empty();

            var profiles = await mediator.Send(GetAgentProfilesQuery.Create(agentEntity.AgentId, since), cancellationToken);

            if(!profiles.Any())
                return MultipleDocumentResult.Empty();

            ICollection<string> ids = profiles.Select(x=> x.Key).ToHashSet();
            DateTimeOffset? lastModified = profiles.OrderByDescending(x=> x.UpdatedAt).Select(x=> x.UpdatedAt).FirstOrDefault();

            return MultipleDocumentResult.Success(ids, lastModified);
        }

        public async Task<IDocument> UpdateAgentProfile(Agent agent, string profileId, byte[] content, string contentType, CancellationToken cancellationToken = default)
        {
            var agentEntity = await mediator.Send(GetAgentQuery.Create(agent), cancellationToken);

            var document = await mediator.Send(UpdateAgentProfileCommand.Create(agentEntity.AgentId, profileId, content, contentType));

            return mapper.Map<IDocument>(document);
        }
    }
}
