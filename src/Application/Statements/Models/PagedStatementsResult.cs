using Doctrina.Domain.Entities;
using System.Collections.Generic;

namespace Doctrina.Application.Statements.Models
{
    public class PagedStatementsResult
    {
        public PagedStatementsResult()
        {
            Statements = new List<StatementEntity>();
            MoreToken = null;
        }

        public PagedStatementsResult(IEnumerable<StatementEntity> statements, string token = null)
        {
            Statements = statements;
            MoreToken = token;
        }

        public IEnumerable<StatementEntity> Statements { get; set; }

        /// <summary>
        /// If token is not null, more statements can be fetched using token
        /// </summary>
        public string MoreToken { get; set; }
    }
}
