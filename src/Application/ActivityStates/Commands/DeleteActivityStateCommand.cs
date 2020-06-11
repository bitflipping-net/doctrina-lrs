using Doctrina.ExperienceApi.Data;
using MediatR;
using System;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class DeleteActivityStateCommand : IRequest
    {
        public string StateId { get; set; }
        public Iri ActivityId { get; set; }
        public Guid AgentId { get; set; }
        public Guid? Registration { get; set; }
    }
}
