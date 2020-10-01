using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class DeleteActivityStateCommand : IRequest
    {
        public string StateId { get; set; }
        public Iri ActivityId { get; set; }
        public PersonaModel Persona { get; set; }
        public Guid? Registration { get; set; }
    }
}
