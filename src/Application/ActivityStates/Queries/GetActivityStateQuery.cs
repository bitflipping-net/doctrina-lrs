using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.ActivityStates.Queries
{
    public class GetActivityStateQuery : IRequest<ActivityStateEntity>
    {
        public string StateId { get; set; }
        public Iri ActivityId { get; set; }
        public Agent Agent { get; set; }
        public Guid? Registration { get; set; }
    }
}
