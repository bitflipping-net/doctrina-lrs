using System.Collections.Generic;

namespace Doctrina.Domain.Entities.InteractionActivities
{
    public abstract class InteractionActivityBase
    {
        public string InteractionType { get; set; }

        public ICollection<string> CorrectResponsesPattern { get; set; }
    }
}
