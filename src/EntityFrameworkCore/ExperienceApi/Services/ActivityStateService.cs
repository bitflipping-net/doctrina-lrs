using Doctrina.Application.ActivityStates.Commands;
using Doctrina.Application.ActivityStates.Queries;
using Doctrina.Application.Agents.Queries;
using Doctrina.Application.Common.Exceptions;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.ExperienceApi.Server.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Services
{
    public class ActivityStateService : IActivityStateService
    {
        private readonly IMediator mediator;

        public ActivityStateService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task DeleteActivityState(string stateId, Iri activityId, Agent agent, Guid? registration, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteActivityStateCommand()
            {
                StateId = stateId,
                ActivityId = activityId,
                Agent = agent,
                Registration = registration
            }, cancellationToken);
        }

        public Task DeleteActivityStates(Iri activityId, Agent agent, Guid? registration, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteActivityStatesCommand()
            {
                ActivityId = activityId,
                Agent = agent,
                Registration = registration
            }, cancellationToken);
        }

        public Task<ActivityStateDocument> GetActivityState(string stateId, Iri activityId, Agent agent, Guid? registration, CancellationToken cancellationToken)
        {
            return mediator.Send(new GetActivityStateQuery()
            {
                StateId = stateId,
                ActivityId = activityId,
                Agent = agent,
                Registration = registration
            }, cancellationToken);
        }

        public async Task<ICollection<ActivityStateDocument>> GetActivityStates(Iri activityId, Agent agent, Guid? registration, DateTime? since, CancellationToken cancellationToken)
        {
            AgentEntity savedAgent = await mediator.Send(GetAgentQuery.Create(agent), cancellationToken);

            if (savedAgent == null)
            {
                return new HashSet<ActivityStateDocument>();
            }

            return await mediator.Send(new GetActivityStatesQuery()
            {
                ActivityId = activityId,
                AgentId = savedAgent.AgentId,
                Registration = registration,
                Since = since
            }, cancellationToken);
        }

        public Task<ActivityStateDocument> PostSingleState(string stateId, Iri activityId, Agent agent, byte[] body, string contentType, Guid? registration, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
