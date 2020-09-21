using System;

namespace Doctrina.Domain.Entities
{
    public class StatementObject
    {
        public Guid StoreId { get; set; }

        public Guid StatementId { get; set; }

        public ObjectType ObjectType { get; set; }

        /// <summary>
        /// The Id of the statement object (Agent, Activity, SubStatement, StatementRef)
        /// </summary>
        public Guid ObjectId { get; set; }
    }
}
