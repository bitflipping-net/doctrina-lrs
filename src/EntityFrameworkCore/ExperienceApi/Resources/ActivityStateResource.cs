using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.ActivityStates.Commands;
using Doctrina.Application.ActivityStates.Queries;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Agents.Queries;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
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
    public class ActivityStateResource : IStateResource
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ActivityStateResource(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public Task DeleteActivityState(string stateId, Iri activityId, Agent agent, Guid? registration = null, CancellationToken cancellationToken = default)
        {
            return mediator.Send(new DeleteActivityStateCommand()
            {
                StateId = stateId,
                ActivityId = activityId,
                Agent = agent,
                Registration = registration
            }, cancellationToken);
        }

        public Task DeleteActivityStates(Iri activityId, Agent agent, Guid? registration = null, CancellationToken cancellationToken = default)
        {
            return mediator.Send(new DeleteActivityStatesCommand()
            {
                ActivityId = activityId,
                Agent = agent,
                Registration = registration
            }, cancellationToken);
        }

        public async Task<IDocument> GetActivityState(string stateId, Iri activityId, Agent agent, Guid? registration = null, CancellationToken cancellationToken = default)
        {
            var state = await mediator.Send(new GetActivityStateQuery()
            {
                StateId = stateId,
                ActivityId = activityId,
                Agent = agent,
                Registration = registration
            }, cancellationToken);

            return mapper.Map<ActivityStateDocument>(state);
        }

        public async Task<MultipleDocumentResult> GetActivityStates(Iri activityId, Agent agent, Guid? registration, DateTimeOffset? since = null, CancellationToken cancellationToken = default)
        {
            AgentEntity savedAgent = await mediator.Send(GetAgentQuery.Create(agent), cancellationToken);

            if (savedAgent == null)
                return MultipleDocumentResult.Empty();

            var states = await mediator.Send(new GetActivityStatesQuery()
            {
                ActivityId = activityId,
                AgentId = savedAgent.AgentId,
                Registration = registration,
                Since = since
            }, cancellationToken);

            if(!states.Any())
                return MultipleDocumentResult.Empty();

            var keys = states.Select(x=> x.Key).ToHashSet();
            var lastModified = states.OrderByDescending(x=> x.UpdatedAt)
                .Select(x=> x.UpdatedAt)
                .FirstOrDefault();

            return MultipleDocumentResult.Success(keys, lastModified);
        }

        public async Task<IDocument> PostSingleState(string stateId, Iri activityId, Agent agent, byte[] body, string contentType, Guid? registration = null, CancellationToken cancellationToken = default)
        {
            var activity = await mediator.Send(UpsertActivityCommand.Create(activityId));
            AgentEntity savedAgent = await mediator.Send(UpsertActorCommand.Create(agent), cancellationToken);

            var document = await mediator.Send(new CreateStateDocumentCommand()
            {
                StateId = stateId,
                ActivityId = activity.ActivityId,
                AgentId = savedAgent.AgentId,
                Content = body,
                ContentType = contentType,
                Registration = registration,
            }, cancellationToken);

            return mapper.Map<IDocument>(document);
        }
    }
}
