using System.Collections.ObjectModel;

namespace Doctrina.Domain.Models.InteractionActivities
{
    public class InteractionComponentCollection : KeyedCollection<string, InteractionComponent>
    {
        protected override string GetKeyForItem(InteractionComponent item)
        {
            return item.Id;
        }
    }
}
