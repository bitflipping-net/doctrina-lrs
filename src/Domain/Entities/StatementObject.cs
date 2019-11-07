using System;

namespace Doctrina.Domain.Entities
{
    public class StatementObjectEntity : IStatementObjectEntity
    {
        //public Guid StatementObjectId { get; set; }
        public EntityObjectType ObjectType { get; set; }
        public virtual AgentEntity Agent { get; set; }
        public virtual ActivityEntity Activity { get; set; }
        public virtual SubStatementEntity SubStatement { get; set; }
        public virtual StatementRefEntity StatementRef { get; set; }
    }
}
