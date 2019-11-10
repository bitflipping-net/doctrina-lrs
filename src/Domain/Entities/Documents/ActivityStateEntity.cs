using System;

namespace Doctrina.Domain.Entities.Documents
{
    public class ActivityStateEntity : IActivityStateEntity
    {
        public ActivityStateEntity()
        {
            Document = new DocumentEntity();
        }

        public ActivityStateEntity(byte[] content, string contentType)
        {
            Document = new DocumentEntity(content, contentType);
        }

        public Guid ActivityStateId { get; set; }
        public string StateId { get; set; }
        public Guid? Registration { get; set; }
        public DocumentEntity Document { get; set; }
        public virtual AgentEntity Agent { get; set; }
        public virtual ActivityEntity Activity { get; set; }
    }
}
