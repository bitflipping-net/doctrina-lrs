using System;

namespace Doctrina.Domain.Entities.Documents
{
    public interface IDocumentEntity
    {
        /// <summary>
        /// Binary contents of the document
        /// To set content, use the UpdateDocument method.
        /// </summary>
        byte[] Content { get; }

        /// <summary>
        /// Content type of the document
        /// To set content type, use the UpdateDocument method.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Checksum to verify data integrity (SHA1)
        /// </summary>
        byte[] Checksum { get; set; }

        /// <summary>
        /// The last date when the document was updated or changed.
        /// </summary>
        DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// The date when the document was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; set; }

        void UpdateDocument(byte[] content, string contentType);
    }
}