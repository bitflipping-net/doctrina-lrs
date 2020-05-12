using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Interfaces;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Agents.Commands
{
    public class UpsertActorCommand : IRequest<IAgentEntity>
    {
        public IAgent Actor { get; private set; }

        public class Handler : IRequestHandler<UpsertActorCommand, IAgentEntity>
        {
            private readonly IDoctrinaDbContext _context;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public Handler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
            {
                _context = context;
                _mediator = mediator;
                _mapper = mapper;
            }

            public async Task<IAgentEntity> Handle(UpsertActorCommand request, CancellationToken cancellationToken)
            {
                return await MergeActor(MapToEntity(request.Actor), cancellationToken);
            }

            private AgentEntity MapToEntity(IAgent actor)
            {
                if(actor.ObjectType == ObjectType.Agent)
                {
                    return _mapper.Map<AgentEntity>(actor);
                }
                else
                {
                    return _mapper.Map<GroupEntity>(actor);
                }
            }

            /// <summary>
            /// Creates or gets current agent
            /// </summary>
            /// <param name="actor"></param>
            /// <returns></returns>
            private async Task<AgentEntity> MergeActor(AgentEntity actor, CancellationToken cancellationToken)
            {
                // Get from db
                actor = await _context.Agents.FirstOrDefaultAsync(x => 
                    x.ObjectType == actor.ObjectType 
                    && x.Hash == actor.Hash,
                    cancellationToken) ?? actor;

                if (actor is GroupEntity group)
                {
                    // Perform group update logic, add group member etc.
                    var remove = new HashSet<AgentEntity>();
                    var groupMembers = new HashSet<AgentEntity>();

                    foreach (var member in group.Members)
                    {
                        var savedGrpActor = await MergeActor(member, cancellationToken);
                        if(savedGrpActor != null)
                        {
                            groupMembers.Add(savedGrpActor);
                        }
                        else
                        {
                            groupMembers.Add(member);
                        }
                    }
                    // Re-create the list of members
                    group.Members = new HashSet<AgentEntity>();
                    foreach (var member in groupMembers)
                    {
                        group.Members.Add(member);
                    }
                    //foreach (var member in group.Members)
                    //{
                    //    // Ensure Agent exist
                    //    var grpAgent = await MergeActor(member, cancellationToken);
                    //    if(grpAgent != null)
                    //    {
                    //        member = grpAgent;
                    //    }

                    //    // Check if the relation exist
                    //    var isMember = group.Members.Count(x => x.Hash == grpAgent.Hash) > 0;
                    //    if (!isMember)
                    //    {
                    //        group.Members.Add(grpAgent);
                    //    }
                    //}
                }

                if (actor.AgentId.Equals(Guid.Empty))
                {
                    actor.AgentId = Guid.NewGuid();
                    _context.Agents.Add(actor);
                }

                return actor;
            }
        }

        public static UpsertActorCommand Create(IAgent agent)
        {
            return new UpsertActorCommand()
            {
                Actor = agent
            };
        }
    }
}
