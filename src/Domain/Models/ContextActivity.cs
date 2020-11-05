using System;

namespace Doctrina.Domain.Models
{
    public class ContextActivity
    {
        public Guid ContextId { get; set; }
        public ContextModel Context { get; set; }

        public ContextType ContextType { get; set; }

        public Guid ActivityId { get; set; }
        public ActivityModel Activity { get; set; }
    }
}
