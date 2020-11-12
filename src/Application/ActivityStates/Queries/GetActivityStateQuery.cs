using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using System;

namespace Doctrina.Application.ActivityStates.Queries
{
    public class GetActivityStateQuery : IRequest<ActivityStateDocument>
    {
        public string StateId { get; set; }
        public Iri ActivityId { get; set; }
        public Agent Agent { get; set; }
        public Guid? Registration { get; set; }
    }
}
