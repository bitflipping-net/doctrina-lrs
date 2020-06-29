using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Doctrina.ExperienceApi.Data;
using Doctrina.MongoDB.Persistence;
using MongoDB.Driver;
using System.Linq;

namespace Doctrina.Application.Statements.Notifications
{
    public class VoidStatementHandler : INotificationHandler<StatementAdded>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;

        public VoidStatementHandler(IDoctrinaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task Handle(StatementAdded notification, CancellationToken cancellationToken)
        {
            var voidingStatement = notification.Entity;
            if(voidingStatement.Verb.Id == KnownVerbs.Voided)
            {
                var @object = voidingStatement.Object;
                if (@object.ObjectType == EntityObjectType.StatementRef)
                {
                    var statementId = @object.StatementRef.StatementId;
                    var voidedStatement = (await _context.Statements
                        .Find(x => x.StatementId == statementId)
                        .ToListAsync(cancellationToken))
                        .FirstOrDefault();

                    if(voidedStatement != null
                        && voidedStatement.Id != KnownVerbs.Voided)
                    {
                        voidedStatement.VoidingStatementId = voidingStatement.StatementId;
                    }
                }
            }
        }
    }
}
