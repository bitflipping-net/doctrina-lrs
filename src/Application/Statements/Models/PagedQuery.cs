using Doctrina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Models
{
    public class PagedQuery
    {
        public StatementEntity Statement { get; set; }
        public long TotalCount { get; set; }
    }
}
