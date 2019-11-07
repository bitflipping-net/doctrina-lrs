using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.ActivityProfiles.Commands
{
    public class DeleteActivityProfileCommand : IRequest
    {
        public Iri ActivityId { get; set; }
        public string ProfileId { get; set; }
        public Guid? Registration { get; set; }
    }
}
