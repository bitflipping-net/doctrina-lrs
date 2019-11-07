using System.Collections.Generic;

namespace Doctrina.Domain.Entities.InteractionActivities
{
    public class SequencingInteractionActivity : InteractionActivityBase
    {
        public InteractionComponentCollection Choices { get; set; }
    }
}
