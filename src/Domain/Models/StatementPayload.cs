using System;

namespace Doctrina.Domain.Models
{
    /// <summary>
    /// Represents the received payload of the statement
    /// </summary>
    public class StatementEncoded : IStoreEntity
    {
        public Guid StoreId { get; set; }

        public long EncodedId { get; set; }

        /// <summary>
        /// JSON encoded string
        /// </summary>
        public string Payload { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public int StatementId { get; set; }

        public StatementModel Statement { get; set; }
    }
}
