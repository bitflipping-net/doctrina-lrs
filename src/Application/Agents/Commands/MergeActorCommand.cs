using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Agents.Commands
{
    public class MergeActorCommand : IRequest<IAgentEntity>
    {
        public IAgent Agent { get; set; }

        public class Handler : IRequestHandler<MergeActorCommand, IAgentEntity>
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

            public async Task<IAgentEntity> Handle(MergeActorCommand request, CancellationToken cancellationToken)
            {
                var newAgent = _mapper.Map<AgentEntity>(request.Agent);
                var result = await MergeActor(newAgent, cancellationToken);
                return result;
            }

            /// <summary>
            /// Creates or gets current agent
            /// </summary>
            /// <param name="agent"></param>
            /// <returns></returns>
            private async Task<AgentEntity> MergeActor(AgentEntity agent, CancellationToken cancellationToken)
            {
                // Get from db
                var currentAgent = await _context.Agents.FirstOrDefaultAsync(x => 
                    x.ObjectType == agent.ObjectType 
                    && x.Hash == agent.Hash,
                    cancellationToken);

                if (currentAgent != null)
                {
                    if (currentAgent is GroupEntity group)
                    {
                        // Perform group update logic, add group member etc.
                        foreach (var member in group.Members)
                        {
                            // Ensure Agent exist
                            var grpAgent = await MergeActor(member, cancellationToken);

                            // Check if the relation exist
                            var isMember = group.Members.Count(x => x.Hash == grpAgent.Hash) > 0;
                            if (!isMember)
                            {
                                group.Members.Add(grpAgent);
                            }
                        }
                    }

                    return currentAgent;
                }
                else
                {
                    // New agent, or anonomouys group
                    _context.Agents.Add(agent);
                    return agent;
                }
            }
        }

        internal static MergeActorCommand Create(IAgent agent)
        {
            return new MergeActorCommand()
            {
                Agent = agent
            };
        }
    }
}
