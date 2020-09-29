using System;

namespace Doctrina.Domain.Models.Documents
{
    public class ActivityProfileEntity : DocumentEntity, IActivityProfileEntity
    {
        public ActivityProfileEntity()
        {
        }

        public ActivityProfileEntity(byte[] body, string contentType) : base(body, contentType)
        {
        }
    }
}
