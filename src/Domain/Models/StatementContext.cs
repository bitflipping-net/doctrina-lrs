using Doctrina.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;

namespace Doctrina.Domain.Models
{
    public class StatementContext: IStoreEntity
    {
        public Guid ContextId { get; set; }
        public Guid? Registration { get; set; }
        public PersonaModel Instructor { get; set; }
        public PersonaModel Team { get; set; }
        public string Revision { get; set; }
        public string Platform { get; set; }
        public string Language { get; set; }
        public ExtensionsCollection Extensions { get; set; }
        public ICollection<ContextActivity> ContextActivities { get; set; }
        public Guid StoreId { get; set; }
    }
}
