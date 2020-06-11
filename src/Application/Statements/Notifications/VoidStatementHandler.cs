using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Doctrina.ExperienceApi.Data;

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
                    var voidedStatement = await _context.Statements
                        .Include(x=> x.Verb)
                        .FirstOrDefaultAsync(x => x.StatementId == statementId, cancellationToken);

                    if(voidedStatement != null
                        && voidedStatement.Verb.Id != KnownVerbs.Voided)
                    {
                        voidedStatement.VoidingStatementId = voidingStatement.StatementId;
                    }
                }
            }
        }
    }
}
