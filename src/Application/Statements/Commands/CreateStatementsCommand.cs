using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Interfaces;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Commands
{
    public class CreateStatementsCommand : IRequest<ICollection<Guid>>
    {
        public CreateStatementsCommand()
        {
            Statements = new StatementCollection();
        }

        public ICollection<Statement> Statements { get; internal set; }

        public static CreateStatementsCommand Create(StatementCollection statements)
        {
            return new CreateStatementsCommand()
            {
                Statements = statements
            };
        }

        public class Handler : IRequestHandler<CreateStatementsCommand, ICollection<Guid>>
        {
            private readonly IMediator _mediator;
            private readonly IDoctrinaDbContext _context;

            public Handler(IDoctrinaDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<ICollection<Guid>> Handle(CreateStatementsCommand request, CancellationToken cancellationToken)
            {
                var ids = new HashSet<Guid>();
                foreach (var statement in request.Statements)
                {
                    ids.Add(await _mediator.Send(CreateStatementCommand.Create(statement), cancellationToken));
                }

                await _context.SaveChangesAsync(cancellationToken);

                return ids;
            }
        }
    }
}
