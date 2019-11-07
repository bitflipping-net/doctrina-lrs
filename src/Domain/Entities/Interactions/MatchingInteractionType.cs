using System.Collections.Generic;

namespace Doctrina.Domain.Entities.InteractionActivities
{
    public class MatchingInteractionActivity : InteractionActivityBase
    {
        public InteractionComponentCollection Source { get; set; }
        public InteractionComponentCollection Target { get; set; }
    }
}
