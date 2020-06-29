using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Models;
using Doctrina.Application.Statements.Notifications;
using Doctrina.Application.SubStatements.Commands;
using Doctrina.Application.Verbs.Commands;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Doctrina.MongoDB.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

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
            // Prepare statement for mapping
            if (request.Statement.Id.HasValue)
            {
                Guid id = request.Statement.Id.Value;
                bool any = await _context.Statements.One();
                ).ConfigureAwait(false);
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
            newStatement.Verb = (VerbEntity)await _mediator.Send(UpsertVerbCommand.Create(request.Statement.Verb), cancellationToken).ConfigureAwait(false);
            newStatement.Actor = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.Statement.Actor), cancellationToken).ConfigureAwait(false);
            newStatement.Authority = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.Statement.Authority), cancellationToken).ConfigureAwait(false);

            if (request.Statement.Context != null)
            {
                newStatement.Context = _mapper.Map<ContextEntity>(request.Statement.Context);
                ContextEntity context = newStatement.Context;
                if (context.Instructor != null)
                {
                    context.Instructor = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.Statement.Context.Instructor), cancellationToken).ConfigureAwait(false);
                }
                if (context.Team != null)
                {
                    context.Team = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.Statement.Context.Team), cancellationToken).ConfigureAwait(false);
                }
            }

            var objType = request.Statement.Object.ObjectType;
            newStatement.Object = new StatementObjectEntity();
            if (objType == ObjectType.Activity)
            {
                newStatement.Object.Activity = (ActivityEntity)await _mediator.Send(UpsertActivityCommand.Create((Activity)request.Statement.Object), cancellationToken).ConfigureAwait(false);
            }
            else if (objType == ObjectType.Agent || objType == ObjectType.Group)
            {
                newStatement.Object.Agent = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create((Agent)request.Statement.Object), cancellationToken).ConfigureAwait(false);
            }
            else if (objType == ObjectType.SubStatement)
            {
                newStatement.Object.SubStatement = (SubStatementEntity)await _mediator.Send(CreateSubStatementCommand.Create((SubStatement)request.Statement.Object), cancellationToken).ConfigureAwait(false);
            }
            else if (objType == ObjectType.StatementRef)
            {
                newStatement.Object.StatementRef = _mapper.Map<StatementRefEntity>((StatementRef)request.Statement.Object);
            }

            if (request.Statement.Result != null)
            {
                newStatement.Result = _mapper.Map<ResultEntity>(request.Statement.Result);
            }
            newStatement.Stored = request.Statement.Stored;
            newStatement.Timestamp = request.Statement.Timestamp;
            newStatement.Version = request.Statement.Version.ToString();
            newStatement.FullStatement = request.Statement.ToJson();

            _context.Statements.Add(newStatement);

            // await _context.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(StatementAdded.Create(newStatement), cancellationToken).ConfigureAwait(false);

            if(request.Persist)
            {
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

            return newStatement.StatementId;
        }
    }
}
