using System;

namespace Doctrina.Domain.Entities.Interfaces
{
    public interface IStatementRefEntity
    {
        /// <summary>
        /// Primary key
        /// </summary>
        Guid StatementRefId { get; set; }
        Guid StatementId { get; set; }
    }
}
