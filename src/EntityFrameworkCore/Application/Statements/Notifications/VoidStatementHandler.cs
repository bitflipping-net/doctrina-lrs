using Doctrina.Domain.Entities;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Notifications
{
    public class VoidStatementHandler : INotificationHandler<StatementCreated>
    {
        private const string VoidedVerb = "http://adlnet.gov/expapi/verbs/voided";
        private readonly IDoctrinaDbContext _context;

        public VoidStatementHandler(IDoctrinaDbContext context)
        {
            _context = context;
        }

        public async Task Handle(StatementCreated notification, CancellationToken cancellationToken)
        {
            var entity = notification.Created;
            if (entity.Verb.Id == VoidedVerb)
            {
                if (entity.ObjectType == EntityObjectType.StatementRef)
                {
                    Guid statementId = entity.ObjectId;

                    StatementEntity statement = await _context.Statements
                        .Include(x => x.Verb)
                        .SingleOrDefaultAsync(x => 
                        x.StatementId == statementId
                    , cancellationToken);

                    if (statement != null
                        && statement.Verb.Id != VoidedVerb)
                    {
                        statement.VoidingStatementId = entity.StatementId;
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
            }
            else
            {
                // Detect if current statement has already been voided
                var voidingStatement = await _context.Statements.SingleOrDefaultAsync(x =>
                    x.ObjectType == EntityObjectType.StatementRef
                    && x.ObjectId == entity.StatementId
                    && x.Verb.Id == VoidedVerb
                , cancellationToken);

                if (voidingStatement != null)
                {
                    entity.VoidingStatement = entity;
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}
