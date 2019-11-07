using System;

namespace Doctrina.Domain.Entities
{
    public class ActivityEntity : IStatementObjectEntity
    {
        public EntityObjectType ObjectType => EntityObjectType.Activity;

        /// <summary>
        /// Entity Id
        /// </summary>
        public Guid ActivityId { get; set; }

        /// <summary>
        /// Hash of <see cref="Id"/>
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Actual absolute Iri
        /// </summary>
        public string Id { get; set; }

        public ActivityDefinitionEntity Definition { get; set; }
    }
}
