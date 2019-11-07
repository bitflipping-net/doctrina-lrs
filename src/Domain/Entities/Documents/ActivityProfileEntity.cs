using System;

namespace Doctrina.Domain.Entities.Documents
{
    public class ActivityProfileEntity : IActivityProfileEntity
    {
        public ActivityProfileEntity()
        {
            Document = new DocumentEntity();
        }

        public ActivityProfileEntity(byte[] content, string contentType)
        {
            Document = new DocumentEntity(content, contentType);
        }


        public Guid ActivityProfileId { get; set; }
        public string ProfileId { get; set; }

        /// <summary>
        /// MD5 checksum of Iri
        /// </summary>
        public virtual ActivityEntity Activity { get; set; }

        public Guid? RegistrationId { get; set; }

        public DocumentEntity Document { get; set; }
    }
}
