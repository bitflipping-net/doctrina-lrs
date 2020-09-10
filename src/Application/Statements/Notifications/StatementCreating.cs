using MediatR;

namespace Doctrina.Application.Statements.Notifications
{
    public class StatementCreating : INotification
    {
        public static StatementCreating Create()
        {
            return new StatementCreating();
        }
    }
}
