using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.SubStatements.Commands;
using Doctrina.Application.Verbs.Commands;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Interfaces;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System;
using System.Threading;
using Doctrina.Persistence.Infrastructure;
using System.Threading.Tasks;

namespace Doctrina.Application.SubStatements
{
    public class CreateSubStatementCommandHandler : IRequestHandler<CreateSubStatementCommand, SubStatementEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateSubStatementCommandHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<SubStatementEntity> Handle(CreateSubStatementCommand request, CancellationToken cancellationToken)
        {
            var subStatement = _mapper.Map<SubStatementEntity>(request.SubStatement);
            subStatement.Timestamp = subStatement.Timestamp ?? DateTimeOffset.UtcNow;

            subStatement.Verb = (VerbEntity)await _mediator.Send(UpsertVerbCommand.Create(request.SubStatement.Verb));
            subStatement.Actor = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.SubStatement.Actor));

            if (subStatement.Context != null)
            {
                var context = subStatement.Context;
                if (context.Instructor != null)
                {
                    context.Instructor = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.SubStatement.Context.Instructor), cancellationToken);
                }

                if (context.Team != null)
                {
                    context.Team = (AgentEntity)await _mediator.Send(UpsertActorCommand.Create(request.SubStatement.Context.Team), cancellationToken);
                }
            }

            var objType = subStatement.Object.ObjectType;
            if (objType == EntityObjectType.Activity)
            {
                subStatement.Object.Activity = (ActivityEntity)await _mediator.Send(UpsertActivityCommand.Create((Activity)request.SubStatement.Object));
            }
            else if (objType == EntityObjectType.Agent || objType == EntityObjectType.Group)
            {
                subStatement.Object.Agent = await _mediator.Send(UpsertActorCommand.Create((Agent)request.SubStatement.Object));
            }
            else if (objType == EntityObjectType.StatementRef)
            {
                // It's already mapped from automapper
                // TODO: Additional logic should be performed here
            }
            _context.SubStatements.Add(subStatement);

            return subStatement;
        }
    }
}
