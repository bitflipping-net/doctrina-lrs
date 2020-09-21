using Doctrina.Domain.Entities.InteractionActivities;
using Doctrina.Domain.Entities.ValueObjects;
using System;

namespace Doctrina.Domain.Entities
{
    /// <summary>
    /// Definition for an Activity used by a statement
    /// </summary>
    public class ActivityDefinitionEntity : IActivityDefinitionEntity
    {
        public Guid StatementId { get; set; }
        public Guid ActivityId { get; set; }
        public Guid StoreId { get; set; }

        public LanguageMapCollection Names { get; set; }

        public LanguageMapCollection Descriptions { get; set; }

        public string Type { get; set; }

        public string MoreInfo { get; set; }

        public InteractionActivityBase InteractionActivity { get; set; }

        public ExtensionsCollection Extensions { get; set; }

        //public string ActivityHash { get; set; }
        //public virtual ActivityEntity Activity { get; set; }
    }
}
