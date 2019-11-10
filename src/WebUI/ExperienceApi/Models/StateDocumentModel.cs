using Doctrina.ExperienceApi.Data;
using Doctrina.WebUI.ExperienceApi.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Doctrina.WebUI.ExperienceApi.Models
{
    [ModelBinder(typeof(ActivityStateModelBinder))]
    public class StateDocumentModel
    {

        public string StateId { get; set; }
        public Iri ActivityId { get; set; }
        public Agent Agent { get; set; }
        public Guid? Registration { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
