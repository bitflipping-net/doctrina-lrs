using AutoMapper;
using Doctrina.Application.Agents.Notifications;
using Doctrina.Application.Agents.Queries;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            AgentEntity persona = await _mediator.Send(GetAgentQuery.Create(request.Actor), cancellationToken);
            bool isNew = false;
            if (persona == null)
            {
                persona = (request.Actor.ObjectType == ObjectType.Agent
                    ? _mapper.Map<AgentEntity>(request.Actor)
                    : _mapper.Map<GroupEntity>(request.Actor));
                persona.AgentId = Guid.NewGuid();

                if (persona.ObjectType == EntityObjectType.Agent
                    && !string.IsNullOrEmpty(request.Actor.Name))
                {
                    persona.Person = new PersonEntity()
                    {
                        PersonId = Guid.NewGuid(),
                        Name = request.Actor.Name
                    };
                }

                _context.Agents.Add(persona);
                await _context.SaveChangesAsync(cancellationToken);
                isNew = true;
            }

            if (!isNew)
            {
                if (request.Actor is Group group
                && !group.IsAnonymous()
                && persona is GroupEntity groupEntity)
                {
                    var upserts = group.Member.Select(member => _mediator.Send(UpsertActorCommand.Create(member), cancellationToken));
                    var members = await Task.WhenAll(upserts);

                    // Remove any members that does not exist in the request group
                    foreach (var member in groupEntity.Members)
                    {
                        if (!members.Any(x => x.AgentId == member.AgentId))
                        {
                            groupEntity.Members.Remove(member);
                        }
                    }

                    // Add any member that does not exist in the stored group from the request group
                    foreach (var member in members)
                    {
                        if (!groupEntity.Members.Any(x => x.AgentId == member.AgentId))
                        {
                            groupEntity.Members.Add(new GroupMemberEntity()
                            {
                                GroupId = groupEntity.AgentId,
                                AgentId = member.AgentId
                            });
                        }
                    }

                    await _mediator.Publish(AgentUpdated.Create(persona));
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return persona;
        }
    }
}