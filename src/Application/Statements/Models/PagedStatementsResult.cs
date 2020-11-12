using Doctrina.Domain.Entities;
using System.Collections.Generic;

namespace Doctrina.Application.Statements.Models
{
    public class PagedStatementsResult
    {
        public PagedStatementsResult()
        {
            Statements = new List<StatementEntity>();
            Cursor = null;
        }

        public PagedStatementsResult(IEnumerable<StatementEntity> statements, string cursor = null)
        {
            Statements = statements;
            Cursor = cursor;
        }

        public IEnumerable<StatementEntity> Statements { get; set; }

        public string Cursor { get; set; }
    }
}
