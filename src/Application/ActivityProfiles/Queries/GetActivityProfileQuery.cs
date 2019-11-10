using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using System;

namespace Doctrina.Application.ActivityProfiles.Queries
{
    public class GetActivityProfileQuery : IRequest<ActivityProfileDocument>
    {
        public string ProfileId { get; set; }
        public Iri ActivityId { get; set; }
        public Guid? Registration { get; set; }
    }
}
