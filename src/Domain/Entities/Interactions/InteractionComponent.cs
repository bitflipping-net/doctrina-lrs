using Doctrina.Domain.Entities.OwnedTypes;

namespace Doctrina.Domain.Entities.InteractionActivities
{
    public class InteractionComponent
    {
        public string Id { get; set; }

        public LanguageMapCollection Description { get; set; }
    }
}
