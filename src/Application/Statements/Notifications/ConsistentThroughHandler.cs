using Doctrina.Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Notifications
{
    public class ConsistentThroughHandler : INotificationHandler<StatementCreated>
    {
        private readonly IDoctrinaAppContext _doctrinaAppContext;

        public ConsistentThroughHandler(IDoctrinaAppContext doctrinaAppContext)
        {
            _doctrinaAppContext = doctrinaAppContext;
        }

        public Task Handle(StatementCreated notification, CancellationToken cancellationToken)
        {
            _doctrinaAppContext.ConsistentThroughDate = DateTimeOffset.UtcNow;

            return Task.CompletedTask;
        }
    }
}
