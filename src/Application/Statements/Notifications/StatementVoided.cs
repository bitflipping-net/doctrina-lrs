using Doctrina.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Notifications
{
    public class StatementVoided : INotification
    {
        public StatementModel Voiding { get; private set; }
        public StatementModel Voided { get; private set; }

        public static StatementVoided Create(StatementModel voidingStatement, StatementModel voidedStatement)
        {
            return new StatementVoided()
            {
                Voiding = voidingStatement,
                Voided = voidedStatement
            };
        }
    }
}
