using System;

namespace Doctrina.Domain.Entities
{
    public class ContextActivityEntity
    {
        public Guid ContextId { get; set; }

        public ContextType ContextType { get; set; }

        public Guid ActivityId { get; set; }

        public virtual ActivityEntity Activity { get; set; }
    }
}
