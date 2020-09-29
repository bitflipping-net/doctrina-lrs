namespace Doctrina.Domain.Models.InteractionActivities
{
    public class ChoiceInteractionActivity : InteractionActivityBase
    {
        public InteractionComponentCollection Choices { get; set; }
    }
}
