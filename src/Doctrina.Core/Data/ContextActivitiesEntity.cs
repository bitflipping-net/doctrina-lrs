﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doctrina.Core.Data
{
    public class ContextActivitiesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Key { get; set; }

        public ICollection<ContextActivitiesParent> Parent { get; set; }

        public ICollection<ContextActivitiesGrouping> Grouping { get; set; }

        public ICollection<ContextActivitiesCategory> Category { get; set; }

        public ICollection<ContextActivitiesOther> Other { get; set; }
    }

    public class ContextActivitiesParent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid ContextId { get; set; }

        public string ActivityId { get; set; }
    }

    public class ContextActivitiesGrouping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid ContextId { get; set; }

        public string ActivityId { get; set; }
    }

    public class ContextActivitiesCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid ContextId { get; set; }

        public string ActivityId { get; set; }
    }

    public class ContextActivitiesOther
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid ContextId { get; set; }

        public string ActivityId { get; set; }
    }
}