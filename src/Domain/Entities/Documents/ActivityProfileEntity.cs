using System;

namespace Doctrina.Domain.Entities.Documents
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
