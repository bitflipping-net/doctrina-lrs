namespace Doctrina.Domain.Entities.InteractionActivities
{
    public abstract class InteractionActivityBase
    {
        public string InteractionType { get; set; }

        public string[] CorrectResponsesPattern { get; set; }
    }
}
