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
                int count = await _context.Statements.CountAsync(x => x.StatementId == request.Statement.Id, cancellationToken);
                if (count > 0)
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

            StatementEntity newStatemnt = _mapper.Map<StatementEntity>(request.Statement);
            newStatemnt.FullStatement = request.Statement.ToJson();
            newStatemnt.Verb = (VerbEntity)await _mediator.Send(MergeVerbCommand.Create(request.Statement.Verb), cancellationToken);
            newStatemnt.Actor = (AgentEntity)await _mediator.Send(MergeActorCommand.Create(request.Statement.Actor), cancellationToken);
            newStatemnt.Authority = (AgentEntity)await _mediator.Send(MergeActorCommand.Create(request.Statement.Authority), cancellationToken);

            if(newStatemnt.Context != null)
            {
                var context = newStatemnt.Context;
                if(context.Instructor != null)
                {
                    context.Instructor = (AgentEntity)await _mediator.Send(MergeActorCommand.Create(request.Statement.Context.Instructor), cancellationToken);
                    
                }
                if(context.Team != null)
                {
                    context.Team = (AgentEntity)await _mediator.Send(MergeActorCommand.Create(request.Statement.Context.Team), cancellationToken);
                }
            }

            var objType = newStatemnt.Object.ObjectType;
            if (objType == EntityObjectType.Activity)
            {
                newStatemnt.Object.Activity = (ActivityEntity)await _mediator.Send(MergeActivityCommand.Create((IActivity)request.Statement.Object));
            }
            else if (objType == EntityObjectType.Agent || objType == EntityObjectType.Group)
            {
                newStatemnt.Object.Agent = (AgentEntity)await _mediator.Send(MergeActorCommand.Create((IAgent)request.Statement.Object));
            }
            else if (objType == EntityObjectType.SubStatement)
            {
                newStatemnt.Object.SubStatement = (SubStatementEntity)await _mediator.Send(CreateSubStatementCommand.Create((ISubStatement)request.Statement.Object));
            }
            else if (objType == EntityObjectType.StatementRef)
            {
                // It's already mapped from automapper
                // TODO: Additional logic should be performed here
            }

            _context.Statements.Add(newStatemnt);

            await _mediator.Publish(StatementAdded.Create(newStatemnt), cancellationToken);

            return newStatemnt.StatementId;
        }
    }
}
