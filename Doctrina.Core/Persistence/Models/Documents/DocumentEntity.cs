﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doctrina.Core.Persistence.Models
{
    public class DocumentEntity : IDocumentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(255)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        [Required]
        [StringLength(50)]
        public string ETag { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}
