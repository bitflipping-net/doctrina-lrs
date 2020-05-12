using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Doctrina.Domain.Entities.OwnedTypes
{
    [Owned]
    public class ContextActivityEntity : IEquatable<ContextActivityEntity>
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Activity IRL ID
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// Activity Hash of IRL ID
        /// </summary>
        public string Hash { get; set; }

        public bool Equals([AllowNull] ContextActivityEntity other)
        {
            return Hash == other?.Hash;
        }
    }
}
