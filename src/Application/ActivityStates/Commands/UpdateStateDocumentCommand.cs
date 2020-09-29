﻿using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using System;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class UpdateStateDocumentCommand : IRequest<ActivityStateDocument>
    {
        public string StateId { get; set; }
        public Iri ActivityId { get; set; }
        public Persona Persona { get; set; }
        public Guid? Registration { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
