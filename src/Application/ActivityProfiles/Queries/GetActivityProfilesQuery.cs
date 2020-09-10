using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;
using System.Collections.Generic;

namespace Doctrina.Application.ActivityProfiles.Queries
{
    public class GetActivityProfilesQuery : IRequest<ICollection<ActivityProfileEntity>>
    {
        public Iri ActivityId { get; set; }
        public DateTimeOffset? Since { get; set; }

        public static GetActivityProfilesQuery Create(Iri activityId, DateTimeOffset? since)
        {
            return new GetActivityProfilesQuery()
            {
                ActivityId = activityId,
                Since = since
            };
        }
    }
}
