namespace Doctrina.Domain.Models.Interfaces
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
