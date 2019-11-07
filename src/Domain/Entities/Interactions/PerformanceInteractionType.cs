using System.Collections.Generic;

namespace Doctrina.Domain.Entities.InteractionActivities
{
    public class PerformanceInteractionActivity : InteractionActivityBase
    {
        public InteractionComponentCollection Steps { get; set; }
    }
}
