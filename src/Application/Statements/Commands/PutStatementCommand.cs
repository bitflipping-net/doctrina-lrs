using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Queries;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Commands
{
    public class PutStatementCommand : IRequest
    {
        public Guid StatementId { get; private set; }
        public Statement Statement { get; private set; }

        public static PutStatementCommand Create(Guid statementId, Statement statement)
        {
            return new PutStatementCommand()
            {
                StatementId = statementId,
                Statement = statement
            };
        }

        public class Handler : IRequestHandler<PutStatementCommand>
        {
            private readonly IDoctrinaDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IDoctrinaDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(PutStatementCommand request, CancellationToken cancellationToken)
            {
                if (!request.Statement.Id.HasValue)
                {
                    request.Statement.Id = request.StatementId;
                }

                await _mediator.Send(CreateStatementCommand.Create(request.Statement), cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return await Unit.Task;
            }
        }
    }
}
