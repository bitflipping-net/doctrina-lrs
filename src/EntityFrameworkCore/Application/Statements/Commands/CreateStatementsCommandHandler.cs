using Doctrina.Application.Statements.Notifications;
using Doctrina.Persistence;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Commands
{
    public class CreateStatementsCommandHandler : IRequestHandler<CreateStatementsCommand, ICollection<Guid>>
    {
        private readonly IMediator _mediator;
        private readonly DoctrinaDbContext _context;

        public CreateStatementsCommandHandler(DoctrinaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ICollection<Guid>> Handle(CreateStatementsCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var ids = new List<Guid>();
                foreach (var statement in request.Statements)
                {
                    var id = await _mediator.Send(CreateStatementCommand.Create(statement), cancellationToken).ConfigureAwait(false);
                    ids.Add(id);
                }

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return ids;
            }
        }
    }
}
