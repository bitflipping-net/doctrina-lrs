using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Commands;
using Doctrina.Application.Statements.Queries;
using Doctrina.Persistence.Infrastructure;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Commands
{
    public class PutStatementCommandHandler : IRequestHandler<PutStatementCommand>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;

        public PutStatementCommandHandler(IDoctrinaDbContext context, IMediator mediator)
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

            await _mediator.Send(CreateStatementCommand.Create(request.Statement), cancellationToken).ConfigureAwait(false);

            await _context.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }
    }
}
