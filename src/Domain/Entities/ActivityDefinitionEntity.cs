﻿using Doctrina.Domain.Entities.InteractionActivities;
using Doctrina.Domain.Entities.OwnedTypes;
using System;

namespace Doctrina.Domain.Entities
{
    public class ActivityDefinitionEntity : IActivityDefinitionEntity
    {
        public Guid ActivityDefinitionId { get; set; }

        public LanguageMapCollection Names { get; set; }

        public LanguageMapCollection Descriptions { get; set; }

        public string Type { get; set; }

        public string MoreInfo { get; set; }

        public InteractionActivityBase InteractionActivity { get; set; }

        public ExtensionsCollection Extensions { get; set; }

        public Guid StoreId { get; set; }
        //public string ActivityHash { get; set; }
        //public virtual ActivityEntity Activity { get; set; }
    }
}
