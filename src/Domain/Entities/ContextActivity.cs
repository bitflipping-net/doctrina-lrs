using Doctrina.Domain.Entities.OwnedTypes;
using System;

namespace Doctrina.Domain.Entities
{
    public class ContextActivity
    {
        public Guid ContextId { get; set; }

        public ContextType ContextType { get; set; }

        public Guid ActivityId { get; set; }
    }
}
