using Doctrina.Domain.Models;

namespace Doctrina.Application.Statements.Models
{
    public class PagedQuery
    {
        public StatementModel Statement { get; set; }
        public long TotalCount { get; set; }
    }
}
