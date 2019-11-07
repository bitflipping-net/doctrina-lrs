using Doctrina.Domain.Entities.OwnedTypes;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Doctrina.Domain.Entities.InteractionActivities
{
    public class InteractionComponent
    {
        public string Id { get; set; }

        public LanguageMapCollection Description { get; set; }
    }
}
