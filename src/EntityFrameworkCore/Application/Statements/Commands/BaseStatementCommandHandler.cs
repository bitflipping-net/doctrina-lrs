using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.Activities.Queries;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.SubStatements.Commands;
using Doctrina.Application.Verbs.Commands;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Interfaces;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Statements.Commands
{
    public class BaseStatementCommandHandler
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BaseStatementCommandHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            this._mapper = mapper;
        }

        public async Task HandleStatementBase(StatementBase statement, IStatementBaseEntity newStatement, CancellationToken cancellationToken)
        {
            newStatement.Timestamp = statement.Timestamp ?? DateTimeOffset.UtcNow;

            await HandleVerb(statement, newStatement, cancellationToken);
            await HandleActor(statement, newStatement, cancellationToken);
            await HandleObject(statement, newStatement, cancellationToken);
            await HandleContext(statement, newStatement, cancellationToken);
            await HandleResult(statement, newStatement, cancellationToken);
        }

        public Task HandleResult(StatementBase statement, IStatementBaseEntity newStatement, CancellationToken cancellationToken)
        {
            if (statement.Result != null)
            {
                newStatement.Result = _mapper.Map<ResultEntity>(statement.Result);
            }

            return Task.CompletedTask;
        }

        public async Task HandleObject(StatementBase statement, IStatementBaseEntity newStatement, CancellationToken cancellationToken)
        {
            var objType = statement.Object.ObjectType;
            if (objType == ObjectType.Activity)
            {
                var activity = await _mediator.Send(UpsertActivityCommand.Create((Activity)statement.Object), cancellationToken);
                newStatement.ObjectType = EntityObjectType.Activity;
                newStatement.ObjectId = activity.ActivityId;
            }
            else if (objType == ObjectType.Agent || objType == ObjectType.Group)
            {
                AgentEntity agent = await _mediator.Send(UpsertActorCommand.Create((Agent)statement.Object), cancellationToken); ;
                newStatement.ObjectType = EntityObjectType.Agent;
                newStatement.ObjectId = agent.AgentId;
            }
            else if (objType == ObjectType.SubStatement)
            {
                SubStatementEntity subStatement = await _mediator.Send(CreateSubStatementCommand.Create((SubStatement)statement.Object), cancellationToken);
                newStatement.ObjectType = EntityObjectType.SubStatement;
                newStatement.ObjectId = subStatement.SubStatementId;
            }
            else if (objType == ObjectType.StatementRef)
            {
                StatementRef statementRef = (StatementRef)statement.Object;
                newStatement.ObjectType = EntityObjectType.StatementRef;
                newStatement.ObjectId = statementRef.Id;
            }
        }

        public async Task HandleContext(StatementBase statement, IStatementBaseEntity newStatement, CancellationToken cancellationToken)
        {
            if (statement.Context != null)
            {
                newStatement.Context = _mapper.Map<ContextEntity>(statement.Context);
                ContextEntity context = newStatement.Context;
                if (context.Instructor != null)
                {
                    var instructor = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(statement.Context.Instructor), cancellationToken);
                    context.InstructorId = instructor.AgentId;
                    context.Instructor = null;
                }
                if (context.Team != null)
                {
                    var team = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(statement.Context.Team), cancellationToken);
                    context.TeamId = team.AgentId;
                    context.Team = null;
                }

                if (context.ContextActivities != null)
                {
                    foreach (var contextActivity in context.ContextActivities)
                    {
                        Iri id = new Iri(contextActivity.Activity.Id);
                        var activity = await _mediator.Send(UpsertActivityCommand.Create(id), cancellationToken);
                        contextActivity.Activity = null;
                        contextActivity.ActivityId = activity.ActivityId;
                    }
                }
            }
        }

        public async Task HandleVerb(StatementBase statement, IStatementBaseEntity newStatement, CancellationToken cancellationToken)
        {
            var verb = (VerbEntity)await _mediator.Send(UpsertVerbCommand.Create(statement.Verb), cancellationToken);
            newStatement.VerbId = verb.VerbId;
            newStatement.Verb = null;
        }

        public async Task HandleActor(StatementBase statement, IStatementBaseEntity newStatement, CancellationToken cancellationToken)
        {
            var actor = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(statement.Actor), cancellationToken);
            newStatement.ActorId = actor.AgentId;
            newStatement.Actor = null;
        }
    }
}