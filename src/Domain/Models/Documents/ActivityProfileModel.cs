using System;

namespace Doctrina.Domain.Models.Documents
{
    public class ActivityProfileModel : DocumentModel, IActivityProfileEntity
    {
        public ActivityProfileModel()
        {
        }

        public ActivityProfileModel(byte[] body, string contentType) : base(body, contentType)
        {
        }
    }
}
