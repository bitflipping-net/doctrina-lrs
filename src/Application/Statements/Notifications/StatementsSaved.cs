using Doctrina.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Doctrina.Application.Statements.Notifications
{
    public class StatementsSaved : INotification
    {
        public StatementsSaved()
        {
            Entities = new HashSet<StatementEntity>();
        }

        public ICollection<StatementEntity> Entities { get; private set; }

        public static StatementsSaved Create(params StatementEntity[] entities)
        {
            return new StatementsSaved()
            {
                Entities = entities
            };
        }
    }
}
