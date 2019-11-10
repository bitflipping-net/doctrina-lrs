using System;

namespace Doctrina.Domain.Entities.Documents
{
    public interface IActivityProfileEntity
    {
        Guid ActivityProfileId { get; set; }
        string ProfileId { get; set; }
        ActivityEntity Activity { get; set; }
        Guid? RegistrationId { get; set; }
        DocumentEntity Document { get; set; }
    }
}