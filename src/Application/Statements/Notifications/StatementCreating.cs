using Doctrina.Domain.Entities;
using MediatR;

namespace Doctrina.Application.Statements.Models
{
    public class StatementCreating : INotification
    {
        public StatementEntity Entity { get; set; }
    }
}
