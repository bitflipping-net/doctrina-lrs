using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Commands;
using Doctrina.Application.Statements.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Statements.Commands
{
    public class CreateStatementsCommandHandler : IRequestHandler<CreateStatementsCommand, ICollection<Guid>>
    {
        private readonly IMediator _mediator;
        private readonly IDoctrinaDbContext _context;

        public CreateStatementsCommandHandler(IDoctrinaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ICollection<Guid>> Handle(CreateStatementsCommand request, CancellationToken cancellationToken)
        {
            var tasks = new List<Task<Guid>>();
            foreach (var statement in request.Statements)
            {
                tasks.Add(_mediator.Send(CreateStatementCommand.Create(statement), cancellationToken));
            }

            var ids = await Task.WhenAll(tasks);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new StatementsSaved());

            return ids;
        }
    }
}
