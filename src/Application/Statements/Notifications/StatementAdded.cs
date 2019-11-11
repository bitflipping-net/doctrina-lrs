using Doctrina.Domain.Entities;
using MediatR;

namespace Doctrina.Application.Statements.Models
{
    public class StatementAdded : INotification
    {
        public StatementEntity Entity { get; set; }

        internal static StatementAdded Create(StatementEntity newStatemnt)
        {
            return new StatementAdded()
            {
                Entity = newStatemnt
            };
        }
    }
}
