using AutoMapper;
using Doctrina.Application.AgentProfiles.Commands;
using Doctrina.Application.AgentProfiles.Queries;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Agents.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.ExperienceApi.Server.Exceptions;
using Doctrina.ExperienceApi.Server.Resources;
using MediatR;
using System;
using System.Collections.Generic;
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
                throw new NotFoundException("Agent Profile", profileId);

            var profile = await mediator.Send(GetAgentProfileQuery.Create(agentEntity.AgentId, profileId));

            return mapper.Map<IDocument>(profile);
        }

        public async Task<ICollection<IDocument>> GetAgentProfiles(Agent agent, DateTimeOffset? since = null, CancellationToken cancellationToken = default)
        {
            var agentEntity = await mediator.Send(GetAgentQuery.Create(agent), cancellationToken);

            if (agentEntity == null)
                return new HashSet<IDocument>();

            var profiles = await mediator.Send(GetAgentProfilesQuery.Create(agentEntity.AgentId, since), cancellationToken);

            return mapper.Map<ICollection<IDocument>>(profiles);
        }

        public Task<IDocument> UpdateAgentProfile(Agent agent, string profileId, byte[] content, string contentType, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
