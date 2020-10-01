using Doctrina.Domain.Models;
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
        public PersonaModel Persona { get; set; }
        public Guid? RegistrationId { get; set; }
    }
}
