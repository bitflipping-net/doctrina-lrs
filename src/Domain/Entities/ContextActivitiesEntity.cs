using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Doctrina.Domain.Entities
{
    public class ContextActivitiesEntity
    {
        public ContextActivitiesEntity()
        {
            Parent = new HashSet<ContextActivityTypeEntity>();

            Grouping = new HashSet<ContextActivityTypeEntity>();

            Category = new HashSet<ContextActivityTypeEntity>();

            Other = new HashSet<ContextActivityTypeEntity>();
        }

        public Guid ContextActivitiesId { get; set; }

        public ICollection<ContextActivityTypeEntity> Parent { get; set; }

        public ICollection<ContextActivityTypeEntity> Grouping { get; set; }

        public ICollection<ContextActivityTypeEntity> Category { get; set; }

        public ICollection<ContextActivityTypeEntity> Other { get; set; }
    }

    public class ContextActivityTypeEntity: IEquatable<ContextActivityTypeEntity>
    {
        /// <summary>
        /// Activity IRL ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Activity Hash of IRL ID
        /// </summary>
        public string Hash { get; set; }

        public bool Equals([AllowNull] ContextActivityTypeEntity other)
        {
            return Hash == other?.Hash;
        }
    }
}
