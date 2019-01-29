﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Doctrina.xAPI.InteractionTypes
{
    public abstract class InteractionTypeBase : ActivityDefinition
    {
        public override Iri Type { get => new Iri("http://adlnet.gov/expapi/activities/cmi.interaction"); set => base.Type = value; }

        protected abstract InteractionType INTERACTION_TYPE { get; }

        [JsonProperty("interactionType",
            NullValueHandling = NullValueHandling.Ignore,
            Required = Required.DisallowNull)]
        [EnumDataType(typeof(InteractionType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public InteractionType InteractionType { get { return this.INTERACTION_TYPE; } }

        [JsonProperty("correctResponsesPattern",
            NullValueHandling = NullValueHandling.Ignore,
            Required = Required.DisallowNull)]
        public string[] CorrectResponsesPattern { get; set; }
    }
}
