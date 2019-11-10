using Doctrina.Domain.Entities.OwnedTypes;
using System;

namespace Doctrina.Domain.Entities
{
    public class ContextEntity
    {
        public Guid ContextId { get; set; }
        public Guid? Registration { get; set; }
        public AgentEntity Instructor { get; set; }
        public AgentEntity Team { get; set; }
        public string Revision { get; set; }
        public string Platform { get; set; }
        public string Language { get; set; }
        public ExtensionsCollection Extensions { get; set; }
        public ContextActivitiesEntity ContextActivities { get; set; }
    }
}
