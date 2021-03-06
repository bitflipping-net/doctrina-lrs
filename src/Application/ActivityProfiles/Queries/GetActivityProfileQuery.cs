﻿using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using System;

namespace Doctrina.Application.ActivityProfiles.Queries
{
    public class GetActivityProfileQuery : IRequest<ActivityProfileEntity>
    {
        public string ProfileId { get; set; }
        public Iri ActivityId { get; set; }
        public Guid? Registration { get; set; }

        public static GetActivityProfileQuery Create(Iri activityId, string profileId, Guid? registration)
        {
            return new GetActivityProfileQuery()
            {
                ProfileId = profileId,
                ActivityId = activityId,
                Registration = registration
            };
        }
    }
}
