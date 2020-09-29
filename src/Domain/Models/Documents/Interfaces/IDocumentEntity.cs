using System;

namespace Doctrina.Domain.Models.Documents
{
    public interface IDocumentEntity
    {
        string Key { get; set; }
        Persona Persona { get;set;}
        ActivityModel Activity { get; set; }
        byte[] Content { get; set; }
        string ContentType { get; set; }
        string Checksum { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        Guid? RegistrationId { get; set; }
    }
}