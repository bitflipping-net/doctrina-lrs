using Doctrina.ExperienceApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Models
{
    public class PagedStatementsResult
    {
        public PagedStatementsResult()
        {
            Statements = new StatementCollection();
            MoreToken = null;
        }

        public PagedStatementsResult(StatementCollection statements, string token)
        {
            Statements = statements;
            MoreToken = token;
        }

        public StatementCollection Statements { get; set; }

        /// <summary>
        /// If token is not null, more statements can be fetched using token
        /// </summary>
        public string MoreToken { get; set; }
    }
}
