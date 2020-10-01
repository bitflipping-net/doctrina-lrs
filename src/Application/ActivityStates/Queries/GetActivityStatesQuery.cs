using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using System;
using System.Collections.Generic;

namespace Doctrina.Application.ActivityStates.Queries
{
    public class GetActivityStatesQuery : IRequest<ICollection<ActivityStateDocument>>
    {
        public Iri ActivityId { get; set; }
        public PersonaModel Persona { get; set; }
        public Guid? Registration { get; set; }
        public DateTime? Since { get; set; }
    }
}
