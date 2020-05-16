using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Statements.Models;
using Doctrina.Application.SubStatements.Commands;
using Doctrina.Application.Verbs.Commands;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
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
                request.Statement.Authority = _authorityContext.Authority;
            }
            else
            {
                // TODO: Validate authority
            }

            StatementEntity newStatement = _mapper.Map<StatementEntity>(request.Statement);
            newStatement.Verb = (VerbEntity)await _mediator.Send(MergeVerbCommand.Create(request.Statement.Verb), cancellationToken).ConfigureAwait(false);
            newStatement.Actor = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.Statement.Actor), cancellationToken).ConfigureAwait(false);
            newStatement.Authority = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.Statement.Authority), cancellationToken).ConfigureAwait(false);

            if(newStatement.Context != null)
            {
                var context = newStatement.Context;
                if(context.Instructor != null)
                {
                    context.Instructor = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.Statement.Context.Instructor), cancellationToken);

                }
                if(context.Team != null)
                {
                    context.Team = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.Statement.Context.Team), cancellationToken);
                }
            }

            var objType = newStatement.Object.ObjectType;
            if (objType == EntityObjectType.Activity)
            {
                newStatement.Object.Activity = (ActivityEntity)await _mediator.Send(MergeActivityCommand.Create((IActivity)request.Statement.Object));
            }
            else if (objType == EntityObjectType.Agent || objType == EntityObjectType.Group)
            {
                newStatement.Object.Agent = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create((IAgent)request.Statement.Object));
            }
            else if (objType == EntityObjectType.SubStatement)
            {
                newStatement.Object.SubStatement = (SubStatementEntity)await _mediator.Send(CreateSubStatementCommand.Create((ISubStatement)request.Statement.Object));
            }
            else if (objType == EntityObjectType.StatementRef)
            {
                // It's already mapped from automapper
                // TODO: Additional logic should be performed here
            }

            newStatement.FullStatement = request.Statement.ToJson();

            _context.Statements.Add(newStatement);

            await _mediator.Publish(StatementAdded.Create(newStatement), cancellationToken);

            return newStatement.StatementId;
        }
    }
}
