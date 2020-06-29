using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Agents.Notifications;
using Doctrina.Application.Agents.Queries;
using Doctrina.Domain.Entities;
using Doctrina.Persistence.Infrastructure;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            // Try find in cache
            AgentEntity actor = await _mediator.Send(GetAgentQuery.Create(request.Actor), cancellationToken);

            if (actor == null)
            {
                actor = (request.Actor.ObjectType == ObjectType.Agent
                    ? _mapper.Map<AgentEntity>(request.Actor)
                    : _mapper.Map<GroupEntity>(request.Actor));
                actor.AgentId = Guid.NewGuid();
                _context.Agents.Add(actor);
            }

            if (request.Actor is Group group && actor is GroupEntity groupEntity)
            {
                if (_context.Entry(actor).State != EntityState.Added)
                {
                    _context.Entry(actor).State = EntityState.Modified;
                }

                // Perform group update logic, add group member etc.
                foreach (var member in group.Member)
                {
                    var savedGrpActor = await _mediator.Send(UpsertActorCommand.Create(member), cancellationToken);

                    if(groupEntity.Members.Any(x=> x.AgentId == savedGrpActor.AgentId))
                    {
                        continue;
                    }

                    groupEntity.Members.Add(new GroupMemberEntity()
                    {
                        GroupMemberId = Guid.NewGuid(),
                        AgentId = savedGrpActor.AgentId,
                        GroupId = groupEntity.AgentId,
                    });
                }

                await _mediator.Publish(AgentUpdated.Create(actor)).ConfigureAwait(false);
            }

            return actor;
        }
    }
}