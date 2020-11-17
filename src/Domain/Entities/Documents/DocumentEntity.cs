using System;
using System.Security.Cryptography;

namespace Doctrina.Domain.Entities.Documents
{
    /// <summary>
    /// Represents a stored document
    /// </summary>
    public class DocumentEntity : IDocumentEntity
    {
        public DocumentEntity() { }

        public DocumentEntity(byte[] content, string contentType)
        {
            Content = content;
            ContentType = contentType;
            UpdatedAt = DateTimeOffset.UtcNow;
            CreatedAt = DateTimeOffset.UtcNow;
            Checksum = GenerateChecksum();
        }

        public long DocumentId { get; set; }

        public string Key { get; set; }

        public Guid? AgentId { get; set; }

        public Guid? RegistrationId { get; set; }

        public Guid? ActivityId { get; set; }

        /// <summary>
        /// Representation of the Content-Type header received
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// The byte array of the document content
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// MD5 Checksum
        /// </summary>
        public string Checksum { get; set; }

        /// <summary>
        /// UTC Date when the document was last modified
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// UTC Date when the document was created
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        // Methods:
        private string GenerateChecksum()
        {
            if (Content == null)
            {
                throw new NullReferenceException("Content is null or empty");
            }

            using (var sha1 = SHA1.Create())
            {
                byte[] checksum = sha1.ComputeHash(Content);
                return BitConverter.ToString(checksum).Replace("-", string.Empty).ToLower();
            }
        }

        public void UpdateDocument(byte[] content, string contentType)
        {
            this.Content = content;
            this.ContentType = contentType;
            this.UpdatedAt = DateTimeOffset.UtcNow;
            Checksum = this.GenerateChecksum();
        }
    }
}
