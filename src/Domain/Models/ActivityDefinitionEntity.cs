using Doctrina.Domain.Models.InteractionActivities;
using Doctrina.Domain.Models.ValueObjects;
using System;

namespace Doctrina.Domain.Models
{
    /// <summary>
    /// Definition for an Activity used by a statement
    /// </summary>
    public class ActivityDefinitionEntity : IActivityDefinitionEntity
    {
        public LanguageMapCollection Names { get; set; }

        public LanguageMapCollection Descriptions { get; set; }

        public string Type { get; set; }

        public string MoreInfo { get; set; }

        /// <summary>
        /// JSON Encoded string
        /// </summary>
        public InteractionActivityBase InteractionActivity { get; set; }

        /// <summary>
        /// JSON Encoded string
        /// </summary>
        public ExtensionsCollection Extensions { get; set; }
    }
}
