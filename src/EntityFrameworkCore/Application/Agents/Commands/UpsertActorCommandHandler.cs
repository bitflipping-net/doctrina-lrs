using AutoMapper;
using Doctrina.Application.Agents.Notifications;
using Doctrina.Application.Agents.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities = Doctrina.Domain.Entities;

namespace Doctrina.Application.Agents.Commands
{
    public class UpsertActorCommandHandler : IRequestHandler<UpsertActorCommand, Entities.AgentEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpsertActorCommandHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Entities.AgentEntity> Handle(UpsertActorCommand request, CancellationToken cancellationToken)
        {
            Entities.AgentEntity actor = await _mediator.Send(GetAgentQuery.Create(request.Actor), cancellationToken);
            bool isNew = false;
            if (actor == null)
            {
                actor = (request.Actor.ObjectType == ObjectType.Agent
                    ? _mapper.Map<Entities.AgentEntity>(request.Actor)
                    : _mapper.Map<Entities.GroupEntity>(request.Actor));
                actor.AgentId = Guid.NewGuid();
                _context.Agents.Add(actor);
                isNew = true;
            }

            if (!isNew)
            {
                if (request.Actor is Group group && actor is Entities.GroupEntity groupEntity)
                {
                    // Perform group update logic, add group member etc.
                    foreach (var member in group.Member)
                    {
                        var savedGrpActor = await _mediator.Send(UpsertActorCommand.Create(member), cancellationToken);

                        if (groupEntity.Members.Any(x => x.AgentId == savedGrpActor.AgentId))
                            continue;

                        groupEntity.Members.Add(new Entities.GroupMemberEntity()
                        {
                            AgentId = savedGrpActor.AgentId,
                            GroupId = groupEntity.AgentId,
                        });
                    }

                    await _mediator.Publish(AgentUpdated.Create(actor));
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return actor;
        }
    }
} 