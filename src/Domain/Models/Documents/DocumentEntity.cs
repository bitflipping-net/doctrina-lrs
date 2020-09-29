using System;
using System.Security.Cryptography;

namespace Doctrina.Domain.Models.Documents
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

        /// <summary>
        /// The unique key for this state
        /// </summary>
        public string Key { get; set; }

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
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// UTC Date when the document was created
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The id of the <see cref="Persona"/>.
        /// </summary>
        public Guid PersonaId { get; set; }

        /// <summary>
        /// Persona identifier this belongs to. (optional)
        /// </summary>
        public Persona Persona { get; set; }

        /// <summary>
        /// The id of the <see cref="Activity"/>.
        /// </summary>
        public Guid ActivityId { get; set; }

        /// <summary>
        /// Activity this belongs to. (optional)
        /// </summary>
        public ActivityModel Activity { get; set; }

        /// <summary>
        /// Registration Id (optional)
        /// </summary>
        public Guid? RegistrationId { get; set; }

        /// <summary>
        /// The id of the <see cref="Store"/>
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// The <see cref="Store"/> this belongs to.
        /// </summary>
        public virtual Store Store { get; set; }

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

        // Factories:
        public void UpdateDocument(byte[] content, string contentType)
        {
            Content = content;
            ContentType = contentType;
            UpdatedAt = DateTimeOffset.UtcNow;
            Checksum = GenerateChecksum();
        }
    }
}
