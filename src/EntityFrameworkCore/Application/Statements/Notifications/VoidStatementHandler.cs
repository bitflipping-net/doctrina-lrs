using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Notifications
{
    public class VoidStatementHandler : INotificationHandler<StatementCreated>
    {
        private const string VerbVoided = KnownVerbs.Voided;
        private readonly IDoctrinaDbContext _context;

        public VoidStatementHandler(IDoctrinaDbContext context)
        {
            _context = context;
        }

        public async Task Handle(StatementCreated notification, CancellationToken cancellationToken)
        {
            var entity = notification.Created;
            if (entity.Verb.Id == VerbVoided)
            {
                StatementRefEntity statementRef = (StatementRefEntity)entity.Object;
                if (statementRef.ObjectType == Domain.Entities.ObjectType.StatementRef)
                {
                    var statementId = statementRef.StatementId;
                    var statement = await _context.Statements
                        .Include(x => x.Verb)
                        .FirstOrDefaultAsync(x => x.StatementId == statementId, cancellationToken);

                    if (statement != null
                        && statement.Verb.Id != VerbVoided)
                    {
                        statement.VoidingStatement = entity;
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
            }
            else
            {
                // Detect if current statement has already been voided
                var voidingStatement = await _context.Statements.SingleOrDefaultAsync(x =>
                    ((StatementRefEntity)x.Object).StatementId == entity.StatementId
                    && x.Verb.Id == VerbVoided,
                    cancellationToken
                );

                if (voidingStatement != null)
                {
                    entity.VoidingStatement = entity;
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}
