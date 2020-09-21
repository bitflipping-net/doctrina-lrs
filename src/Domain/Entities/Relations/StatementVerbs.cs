using System;

namespace Doctrina.Domain.Entities.Relations
{
    public class StatementVerbs
    {
        public Guid StatementId { get; set; }
        public StatementEntity Statement { get; set; }

        public Guid VerbId { get; set; }
        public VerbEntity Verb { get; set; }

        public Guid StoreId { get; set; }
        public Store Store { get; set; }
    }
}
