using System;
using MediatR;

namespace Doctrina.Application.Statements.Notifications
{
    public class StatementsSaved : INotification
    {
        public Guid[] Ids { get; private set; }

        internal static StatementsSaved Create(Guid[] ids)
        {
            return new StatementsSaved()
            {
                Ids = ids
            };
        }
    }
}
