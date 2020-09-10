using Doctrina.Domain.Entities;
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
        public StatementEntity Voiding { get; private set; }
        public StatementEntity Voided { get; private set; }

        public static StatementVoided Create(StatementEntity voidingStatement, StatementEntity voidedStatement)
        {
            return new StatementVoided()
            {
                Voiding = voidingStatement,
                Voided = voidedStatement
            };
        }
    }
}
