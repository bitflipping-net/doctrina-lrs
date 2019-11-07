using System.Collections.ObjectModel;

namespace Doctrina.Domain.Entities.InteractionActivities
{
    public class InteractionComponentCollection : KeyedCollection<string, InteractionComponent>
    {
        protected override string GetKeyForItem(InteractionComponent item)
        {
            return item.Id;
        }
    }
}
