using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Clients.Queries;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Identity;
using Doctrina.Application.Statements.Notifications;
using Doctrina.Application.SubStatements.Commands;
using Doctrina.Application.Verbs.Commands;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Interfaces;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Commands
{
    public class CreateStatementCommandHandler : BaseStatementCommandHandler,
        IRequestHandler<CreateStatementCommand, Guid>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IClientContext _clientContext;

        public CreateStatementCommandHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper, IClientContext clientContext)
        : base(mediator, mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
            _clientContext = clientContext;
        }

        /// <summary>
        /// Creates statement without saving to database
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Guid of the created statement</returns>
        public async Task<Guid> Handle(CreateStatementCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Publish(StatementCreating.Create(), cancellationToken);

            // Prepare statement for mapping
            if (request.Statement.Id.HasValue)
            {
                bool any = await _context.Statements.AnyAsync(x => x.StatementId == request.Statement.Id, cancellationToken);
                if (any)
                {
                    return request.Statement.Id.Value;
                }
            }

            request.Statement.Stamp();

            // Ensure statement version and stored date
            request.Statement.Version = request.Statement.Version ?? ApiVersion.GetLatest().ToString();
            request.Statement.Stored = request.Statement.Stored ?? DateTimeOffset.UtcNow;

            if (request.Statement.Authority == null)
            {
                // Set authority before saving JSON encoded statement
                request.Statement.Authority = _clientContext.GetClientAuthority();
            }
            else
            {
                // TODO: Validate authority
                var client = await _mediator.Send(ClientByAgentQuery.Create(request.Statement.Authority));
            }

            // Start mapping statement
            StatementEntity newStatement = new StatementEntity();
            newStatement.StatementId = request.Statement.Id.GetValueOrDefault();
            newStatement.ClientId = _clientContext.GetClientId();

            await HandleStatementBase(request.Statement, newStatement, cancellationToken);

            newStatement.Stored = request.Statement.Stored;
            newStatement.Timestamp = request.Statement.Timestamp;
            newStatement.Version = request.Statement.Version.ToString();
            newStatement.FullStatement = request.Statement.ToJson();

            _context.Statements.Add(newStatement);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(StatementCreated.Create(newStatement), cancellationToken);

            return newStatement.StatementId;
        }
    }
}