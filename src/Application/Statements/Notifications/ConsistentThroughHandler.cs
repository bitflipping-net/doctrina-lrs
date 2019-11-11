using Doctrina.Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Notifications
{
    public class ConsistentThroughHandler : INotificationHandler<StatementsSaved>
    {
        private readonly IDoctrinaAppContext _doctrinaAppContext;

        public ConsistentThroughHandler(IDoctrinaAppContext doctrinaAppContext)
        {
            _doctrinaAppContext = doctrinaAppContext;
        }

        public Task Handle(StatementsSaved notification, CancellationToken cancellationToken)
        {
            _doctrinaAppContext.ConsistentThroughDate = DateTimeOffset.UtcNow;

            return Task.CompletedTask;
        }
    }
}
