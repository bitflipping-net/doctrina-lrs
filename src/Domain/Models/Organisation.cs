using Doctrina.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Models
{
    public class Organisation
    {
        /// <summary>
        /// The primary key
        /// </summary>
        public Guid OrganisationId { get; set; }

        /// <summary>
        /// The owner/creator of the orgasination
        /// </summary>
        public Guid Owner { get; set; }

        /// <summary>
        /// The name of the orgasination
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The parent orgasination id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// When the organisation was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// When the organisation was last updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Settings for the organisation
        /// </summary>
        public OrganisationSettings Settings { get; set; }

        /// <summary>
        /// Stores in this organisation
        /// </summary>
        public virtual ICollection<Store> Stores { get; set; }

        /// <summary>
        /// People in this organisation
        /// </summary>
        public virtual ICollection<Person> People { get; set; }
    }
}
