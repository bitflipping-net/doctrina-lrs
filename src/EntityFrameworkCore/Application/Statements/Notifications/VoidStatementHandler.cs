using Doctrina.Domain.Entities;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                var statementRef = entity.Object;
                if (statementRef.ObjectType == EntityObjectType.StatementRef)
                {
                    var statementId = statementRef.StatementRef.StatementId;
                    var statement = await _context.Statements
                        .Include(x => x.Verb)
                        .FirstOrDefaultAsync(x => x.StatementId == statementId, cancellationToken);

                    if (statement != null
                        && statement.Verb.Id != VoidedVerb)
                    {
                        statement.VoidingStatement = entity;
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
            }else{
                // Detect if current statement has already been voided
                var voidingStatement = await _context.Statements.SingleOrDefaultAsync(x=> 
                    x.Object.StatementRef.StatementId == entity.StatementId 
                    && x.Verb.Id == VoidedVerb, 
                    cancellationToken
                );

                if(voidingStatement != null) 
                {
                    entity.VoidingStatement = entity;
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}
