using System;

namespace Doctrina.Domain.Entities.Documents
{
    public interface IDocumentEntity
    {
        string Key { get; set; }
        Persona PersonaIdentifier { get;set;}
        ActivityEntity Activity { get; set; }
        byte[] Content { get; set; }
        string ContentType { get; set; }
        string Checksum { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        Guid? RegistrationId { get; set; }
    }
}