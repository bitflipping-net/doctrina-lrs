using Doctrina.Domain.Entities;
using MediatR;

namespace Doctrina.Application.Statements.Notifications
{
    public class StatementCreating : INotification
    {
        public StatementEntity Entity { get; set; }
    }
}
