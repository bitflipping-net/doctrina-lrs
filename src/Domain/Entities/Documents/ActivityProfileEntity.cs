using System;

namespace Doctrina.Domain.Entities.Documents
{
    public class ActivityProfileEntity : IActivityProfileEntity
    {
        public ActivityProfileEntity()
        {
        }

        public ActivityProfileEntity(byte[] body, string contentType)
        {
            Document = new DocumentEntity(body, contentType);
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
