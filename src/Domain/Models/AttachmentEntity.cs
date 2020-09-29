using Doctrina.Domain.Models.ValueObjects;
using System;

namespace Doctrina.Domain.Models
{
    public class AttachmentEntity
    {
        public Guid Id { get; set; }

        public string UsageType { get; set; }

        public LanguageMapCollection Description { get; set; }

        public LanguageMapCollection Display { get; set; }

        public string ContentType { get; set; }

        public byte[] Payload { get; set; }

        public string FileUrl { get; set; }

        public string SHA2 { get; set; }

        public int Length { get; set; }
    }
}
