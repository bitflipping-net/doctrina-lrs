using AutoMapper;
using Doctrina.Application.Agents.Notifications;
using Doctrina.Application.Agents.Queries;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Agents.Commands
{
    public class UpsertActorCommandHandler : IRequestHandler<UpsertActorCommand, AgentEntity>
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

        public async Task<AgentEntity> Handle(UpsertActorCommand request, CancellationToken cancellationToken)
        {
            AgentEntity actor = await _mediator.Send(GetAgentQuery.Create(request.Actor), cancellationToken);
            bool isNew = false;
            if (actor == null)
            {
                actor = (request.Actor.ObjectType == ObjectType.Agent
                    ? _mapper.Map<AgentEntity>(request.Actor)
                    : _mapper.Map<GroupEntity>(request.Actor));
                actor.AgentId = Guid.NewGuid();

                if (!string.IsNullOrEmpty(request.Actor.Name))
                {
                    actor.Person = new PersonEntity()
                    {
                        Name = request.Actor.Name
                    };
                }

                _context.Agents.Add(actor);
                isNew = true;
            }

            if (!isNew)
            {
                if (request.Actor is Group group && actor is GroupEntity groupEntity)
                {
                    // Perform group update logic, add group member etc.
                    foreach (var member in group.Member)
                    {
                        var savedGrpActor = await _mediator.Send(UpsertActorCommand.Create(member), cancellationToken);

                        if (groupEntity.Members.Any(x => x.AgentId == savedGrpActor.AgentId))
                            continue;

                        groupEntity.Members.Add(new GroupMemberEntity()
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