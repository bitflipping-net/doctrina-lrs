using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.ActivityProfiles.Commands
{
    public class DeleteActivityProfileCommand : IRequest
    {
        public Iri ActivityId { get; private set; }
        public string ProfileId { get; private set; }
        public Guid? Registration { get; private set; }

        public static DeleteActivityProfileCommand Create(string profileId, Iri activityId, Guid? registration)
        {
            return new DeleteActivityProfileCommand()
            {
                ProfileId = profileId,
                ActivityId = activityId,
                Registration = registration
            };
        }
    }
}
