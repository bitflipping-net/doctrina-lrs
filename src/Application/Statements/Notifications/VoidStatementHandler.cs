using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Models
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
            var entity = notification.Entity;
            if(entity.Verb.Id == "http://adlnet.gov/expapi/verbs/voided")
            {
                var @object = entity.Object;
                if (@object.ObjectType == EntityObjectType.StatementRef)
                {
                    var statementId = @object.StatementRef.StatementId;
                    var statement = await _context.Statements
                        .Include(x=> x.Verb)
                        .FirstOrDefaultAsync(x => x.StatementId == statementId, cancellationToken);

                    if(statement != null 
                        && statement.Verb.Id != "http://adlnet.gov/expapi/verbs/voided")
                    {
                        statement.Voided = true;
                    }
                }
            }
        }
    }
}
