using Doctrina.Domain.Models.Interfaces;
using System;

namespace Doctrina.Domain.Models
{

    public class StatementRefEntity : IStatementObjectModel
    {
        public Models.ObjectType ObjectType => Models.ObjectType.StatementRef;

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
