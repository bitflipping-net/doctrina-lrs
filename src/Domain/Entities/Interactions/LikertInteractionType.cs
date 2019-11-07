using System.Collections.Generic;

namespace Doctrina.Domain.Entities.InteractionActivities
{
    public class LikertInteractionActivity : InteractionActivityBase
    {
        public InteractionComponentCollection Scale { get; set; }
    }
}
