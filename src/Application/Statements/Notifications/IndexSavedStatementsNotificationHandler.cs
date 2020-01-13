using Doctrina.Application.Common.Interfaces;
using MediatR;
using Nest;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Notifications
{
    public class IndexSavedStatementsNotificationHandler : INotificationHandler<StatementsSaved>
    {
        private readonly ISearchManager _searchManager;

        public IndexSavedStatementsNotificationHandler(ISearchManager searchManager)
        {
            _searchManager = searchManager;
        }

        public async Task Handle(StatementsSaved notification, CancellationToken cancellationToken)
        {
            //await _searchManager.Client.Bulk(notification.Entities, cancellationToken: cancellationToken);
        }
    }
}
