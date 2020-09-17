using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Notifications;
using Doctrina.Application.SubStatements.Commands;
using Doctrina.Application.Verbs.Commands;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Commands
{
    public class CreateStatementCommandHandler : IRequestHandler<CreateStatementCommand, Guid>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IAuthorityContext _authorityContext;

        public CreateStatementCommandHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper, IAuthorityContext currentAuthority)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
            _authorityContext = currentAuthority;
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
                // TODO: Map group?
                request.Statement.Authority = _mapper.Map<Agent>(_authorityContext.Authority);
            }
            else
            {
                // TODO: Validate authority
            }

            // Start mapping statement
            StatementEntity newStatement = new StatementEntity();
            newStatement.StatementId = request.Statement.Id.GetValueOrDefault();
            newStatement.Verb = (VerbEntity)await _mediator.Send(UpsertVerbCommand.Create(request.Statement.Verb), cancellationToken);
            newStatement.Actor = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.Statement.Actor), cancellationToken);
            newStatement.Authority = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.Statement.Authority), cancellationToken);

            if (request.Statement.Context != null)
            {
                newStatement.Context = _mapper.Map<ContextEntity>(request.Statement.Context);
                ContextEntity context = newStatement.Context;
                if (context.Instructor != null)
                {
                    context.Instructor = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.Statement.Context.Instructor), cancellationToken);
                }
                if (context.Team != null)
                {
                    context.Team = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.Statement.Context.Team), cancellationToken);
                }
            }

            var objType = request.Statement.Object.ObjectType;
            if (objType == ExperienceApi.Data.ObjectType.Activity)
            {
                newStatement.Object = (ActivityEntity)await _mediator.Send(UpsertActivityCommand.Create((Activity)request.Statement.Object), cancellationToken);
            }
            else if (objType == ExperienceApi.Data.ObjectType.Agent || objType == ExperienceApi.Data.ObjectType.Group)
            {
                newStatement.Object = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create((Agent)request.Statement.Object), cancellationToken);
            }
            else if (objType == ExperienceApi.Data.ObjectType.SubStatement)
            {
                newStatement.Object = (SubStatementEntity)await _mediator.Send(CreateSubStatementCommand.Create((SubStatement)request.Statement.Object), cancellationToken);
            }
            else if (objType == ExperienceApi.Data.ObjectType.StatementRef)
            {
                newStatement.Object = _mapper.Map<StatementRefEntity>((StatementRef)request.Statement.Object);
            }

            if (request.Statement.Result != null)
            {
                newStatement.Result = _mapper.Map<ResultEntity>(request.Statement.Result);
            }

            newStatement.CreatedAt = request.Statement.Stored;
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