using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Doctrina.Application.Agents.Notifications;
using Doctrina.Application.Agents.Queries;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
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

            if(actor == null)
            {
                actor = (request.Actor.ObjectType == ObjectType.Agent
                    ? _mapper.Map<AgentEntity>(request.Actor)
                    : _mapper.Map<GroupEntity>(request.Actor));
                actor.AgentId = Guid.NewGuid();
                _context.Agents.Add(actor);
            }

            if (request.Actor is Group group && actor is GroupEntity groupEntity)
            {
                if(_context.Entry(actor).State != EntityState.Added)
                {
                    _context.Entry(actor).State = EntityState.Modified;
                }

                // Perform group update logic, add group member etc.
                var remove = new HashSet<AgentEntity>();
                var groupMembers = new HashSet<GroupMemberEntity>();

                foreach (var member in group.Member)
                {
                    var savedGrpActor = await _mediator.Send(UpsertActorCommand.Create(member), cancellationToken);

                    groupMembers.Add(new GroupMemberEntity(){
                        AgentId = savedGrpActor.AgentId,
                        GroupId = groupEntity.AgentId,
                    });
                }
                // Re-create the list of members
                groupEntity.Members = new HashSet<GroupMemberEntity>();
                foreach (var member in groupMembers)
                {
                    groupEntity.Members.Add(member);
                }

                await _mediator.Publish(AgentUpdated.Create(actor)).ConfigureAwait(false);
            }

            return actor;
        }
    }
}