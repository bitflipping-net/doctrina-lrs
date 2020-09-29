namespace Doctrina.Domain.Models.InteractionActivities
{
    public class MatchingInteractionActivity : InteractionActivityBase
    {
        public InteractionComponentCollection Source { get; set; }
        public InteractionComponentCollection Target { get; set; }
    }
}
