using Doctrina.Domain.Entities.OwnedTypes;
using System;

namespace Doctrina.Domain.Entities
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
