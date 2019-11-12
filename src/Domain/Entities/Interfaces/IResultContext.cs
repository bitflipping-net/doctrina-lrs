namespace Doctrina.Domain.Entities.Interfaces
{
    public interface IResultEntity
    {
        bool? Success { get; set; }

        bool? Completion { get; set; }

        string Response { get; set; }

        long? Duration { get; set; }

        string Extensions { get; set; }
    }
}
