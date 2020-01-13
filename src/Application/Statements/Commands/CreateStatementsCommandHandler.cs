using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Commands;
using Doctrina.Application.Statements.Notifications;
using Doctrina.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var tasks = new List<Task<StatementEntity>>();
            foreach (var statement in request.Statements)
            {
                tasks.Add(_mediator.Send(CreateStatementCommand.Create(statement), cancellationToken));
            }

            var statements = await Task.WhenAll(tasks);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(StatementsSaved.Create(statements));

            return statements.Select(x=> x.StatementId).ToHashSet();
        }
    }
}
