using System;

namespace Doctrina.Domain.Entities.Documents
{
    public class ActivityStateEntity : DocumentEntity
    {
        public ActivityStateEntity()
        {
        }

        public ActivityStateEntity(byte[] content, string contentType)
            : base(content, contentType)
        {
        }

        public virtual AgentEntity Agent { get; set; }
        public virtual ActivityEntity Activity { get; set; }
    }
}
