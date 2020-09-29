using Doctrina.Domain.Models;
using System.Collections.Generic;

namespace Doctrina.Application.Statements.Models
{
    public class PagedStatementsResult
    {
        public PagedStatementsResult()
        {
            Statements = new List<StatementModel>();
            MoreToken = null;
        }

        public PagedStatementsResult(IEnumerable<StatementModel> statements, string token = null)
        {
            Statements = statements;
            MoreToken = token;
        }

        public IEnumerable<StatementModel> Statements { get; set; }

        /// <summary>
        /// If token is not null, more statements can be fetched using token
        /// </summary>
        public string MoreToken { get; set; }
    }
}
