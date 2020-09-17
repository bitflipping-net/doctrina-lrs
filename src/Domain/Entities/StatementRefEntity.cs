using Doctrina.Domain.Entities.Interfaces;
using System;

namespace Doctrina.Domain.Entities
{

    public class StatementRefEntity : IStatementObjectEntity, IStatementRefEntity
    {
        public Entities.ObjectType ObjectType => Entities.ObjectType.StatementRef;

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid StatementRefId { get; set; }

        /// <summary>
        /// Id of the referenced statement
        /// </summary>
        public Guid StatementId { get; set; }
    }
}
