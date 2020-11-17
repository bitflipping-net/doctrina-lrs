using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;
using System.Collections.Generic;

namespace Doctrina.Application.ActivityStates.Queries
{
    public class GetActivityStatesQuery : IRequest<ICollection<ActivityStateEntity>>
    {
        public Iri ActivityId { get; set; }
        public Guid AgentId { get; set; }
        public Guid? Registration { get; set; }
        public DateTimeOffset? Since { get; set; }
    }
}
