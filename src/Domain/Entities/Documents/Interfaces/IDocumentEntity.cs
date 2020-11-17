using System;

namespace Doctrina.Domain.Entities.Documents
{
    public interface IDocumentEntity
    {
        byte[] Content { get; set; }
        string ContentType { get; set; }
        string Checksum { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
        DateTimeOffset CreatedAt { get; set; }
    }
}