using Doctrina.Domain.Entities.OwnedTypes;
using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Entities
{
    public class ContextEntity
    {
        public Guid ContextId { get; set; }
        public Guid? Registration { get; set; }
        public Guid? InstructorId { get; set; }
        public virtual AgentEntity Instructor { get; set; }
        public Guid? TeamId { get; set; }
        public virtual AgentEntity Team { get; set; }
        public string Revision { get; set; }
        public string Platform { get; set; }
        public string Language { get; set; }
        public ExtensionsCollection Extensions { get; set; }
        public ICollection<ContextActivityEntity> ContextActivities { get; set; }
    }
}
