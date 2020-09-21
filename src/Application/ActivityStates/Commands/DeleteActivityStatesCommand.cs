using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class DeleteActivityStatesCommand : IRequest
    {
        public Iri ActivityId { get; set; }
        public Persona IFI { get; set; }
        public Guid? Registration { get; set; }
    }
}
