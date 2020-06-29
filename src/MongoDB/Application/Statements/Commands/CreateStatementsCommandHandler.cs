using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Commands;
using Doctrina.Application.Statements.Notifications;
using Doctrina.Domain.Entities;
using Doctrina.MongoDB.Persistence;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Statements.Commands
{
    public class CreateStatementsCommandHandler : IRequestHandler<CreateStatementsCommand, ICollection<Guid>>
    {
        private readonly IMediator _mediator;
        private IMapper _mapper;
        private readonly IDoctrinaDbContext _context;

        public CreateStatementsCommandHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ICollection<Guid>> Handle(CreateStatementsCommand request, CancellationToken cancellationToken)
        {
            var ids = new List<Guid>();
            foreach (var statement in request.Statements)
            {
                statement.Stamp();
                ids.Add(statement.Id.GetValueOrDefault());
            }

            var statements = _mapper.Map<IEnumerable<StatementEntity>>(request.Statements);
            await _context.Statements.InsertManyAsync(statements, null, cancellationToken);

            await _mediator.Publish(StatementsSaved.Create(ids.ToArray()));

            return ids;
        }
    }
}
