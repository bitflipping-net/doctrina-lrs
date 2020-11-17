using System;

namespace Doctrina.Domain.Entities.Documents
{
    public class ActivityProfileEntity : DocumentEntity
    {
        public ActivityProfileEntity()
        {
        }

        public ActivityProfileEntity(byte[] content, string contentType)
            : base(content, contentType)
        {
        }

        public virtual ActivityEntity Activity { get; set; }
    }
}
