using System.Collections.Generic;

namespace Doctrina.Domain.Models.InteractionActivities
{
    public interface IInteractionActivity
    {
        string InteractionType { get; }
        ICollection<string> CorrectResponsesPattern { get; set; }
    }
}
