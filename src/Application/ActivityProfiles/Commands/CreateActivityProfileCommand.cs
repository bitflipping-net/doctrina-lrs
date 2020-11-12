using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.ActivityProfiles.Commands
{
    public class CreateActivityProfileCommand : IRequest<ActivityProfileEntity>
    {
        public Iri ActivityId { get; set; }
        public string ProfileId { get; set; }
        public Guid? Registration { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
