using Doctrina.Domain.Entities;

namespace Doctrina.Application.Statements.Models
{
    public class PagedQuery
    {
        public StatementEntity Statement { get; set; }
        public long TotalCount { get; set; }
    }
}
