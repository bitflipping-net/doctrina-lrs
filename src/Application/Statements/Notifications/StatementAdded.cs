using Doctrina.Domain.Models;
using MediatR;

namespace Doctrina.Application.Statements.Notifications
{
    public class StatementAdded : INotification
    {
        public StatementModel Entity { get; set; }

        public static StatementAdded Create(StatementModel newStatemnt)
        {
            return new StatementAdded()
            {
                Entity = newStatemnt
            };
        }
    }
}
