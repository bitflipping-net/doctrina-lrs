using System.Collections.Generic;

namespace Doctrina.Domain.Entities.InteractionActivities
{
    public class ChoiceInteractionActivity : InteractionActivityBase
    {
        public InteractionComponentCollection Choices { get; set; }
    }
}
