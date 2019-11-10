using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Application.Statements.Commands
{
    public class CreateStatementsCommandHandler : IRequestHandler<CreateStatementsCommand, ICollection<Guid>>
    {
        private readonly IMediator _mediator;
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CreateStatementsCommandHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper, ILogger<CreateStatementsCommand> logger)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<Guid>> Handle(CreateStatementsCommand request, CancellationToken cancellationToken)
        {
            var ids = new HashSet<Guid>();
            foreach (var statement in request.Statements)
            {
                var id = await _mediator.Send(CreateStatementCommand.Create(statement), cancellationToken);
                ids.Add(id);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return ids;
        }
    }
}
