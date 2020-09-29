using System.Collections.Generic;

namespace Doctrina.Domain.Models.InteractionActivities
{

    public abstract class InteractionActivityBase : IInteractionActivity
    {
        public string InteractionType { get; }

        public ICollection<string> CorrectResponsesPattern { get; set; }
    }
}
