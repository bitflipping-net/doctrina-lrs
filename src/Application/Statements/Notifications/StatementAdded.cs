using Doctrina.Domain.Entities;
using MediatR;

namespace Doctrina.Application.Statements.Notifications
{
    public class StatementAdded : INotification
    {
        public StatementEntity Entity { get; set; }

        public static StatementAdded Create(StatementEntity newStatemnt)
        {
            return new StatementAdded()
            {
                Entity = newStatemnt
            };
        }
    }
}
