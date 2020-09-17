using System.Collections.Generic;

namespace Doctrina.Domain.Entities.InteractionActivities
{
    public interface IInteractionActivity
    {
        string InteractionType { get; }
        ICollection<string> CorrectResponsesPattern { get; set; }
    }
}
