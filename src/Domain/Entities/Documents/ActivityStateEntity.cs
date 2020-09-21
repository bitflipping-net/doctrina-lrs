using System;

namespace Doctrina.Domain.Entities.Documents
{
    public class ActivityStateEntity : DocumentEntity, IActivityStateEntity, IDocumentEntity
    {
        public ActivityStateEntity()
        {
        }

        public ActivityStateEntity(byte[] content, string contentType) : base(content, contentType)
        {
        }
    }
}
