using System;

namespace Doctrina.Domain.Models.Documents
{
    public class ActivityStateEntity : DocumentModel, IActivityStateEntity, IDocumentEntity
    {
        public ActivityStateEntity()
        {
        }

        public ActivityStateEntity(byte[] content, string contentType) : base(content, contentType)
        {
        }

        public string StateId { get { return Key; } set { Key = value; } }
    }
}
