using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Extensions;
using Doctrina.ExperienceApi.Data;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Agents.Commands
{
    public class MergeActorCommand : IRequest<AgentEntity>
    {
        public AgentEntity Actor { get; private set; }

        public class Handler : IRequestHandler<MergeActorCommand, AgentEntity>
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

            public Task<AgentEntity> Handle(MergeActorCommand request, CancellationToken cancellationToken)
            {
                var agent = request.Actor;

                MergeActor(agent);

                return Task.FromResult(agent);
            }

            /// <summary>
            /// Creates or gets current agent
            /// </summary>
            /// <param name="agent"></param>
            /// <returns></returns>
            private AgentEntity MergeActor(AgentEntity agent)
            {
                // Get from db
                var currentAgent = _context.Agents.WhereAgent(x => x, agent).FirstOrDefault();

                if (currentAgent != null)
                {
                    GroupEntity group = currentAgent as GroupEntity;
                    if(group != null)
                    {
                        // Perform group update logic, add group member etc.
                        foreach (var member in group.Members)
                        {
                            // Ensure Agent exist
                            var grpAgent = MergeActor(member);

                            // Check if the relation exist
                            var isMember = group.Members.WhereAgent(x => x, grpAgent).Count() > 0;
                            if (!isMember)
                            {
                                group.Members.Add(grpAgent);
                            }
                        }
                    }

                    return currentAgent;
                }

                // New agent, or anonomouys group
                _context.Agents.Add(agent);

                return agent;
            }
        }

        public static MergeActorCommand Create(IMapper mapper, Agent actor)
        {
            var cmd = new MergeActorCommand()
            {
                Actor = mapper.Map<AgentEntity>(actor)
            };

            return cmd;
        }

        public static MergeActorCommand Create(AgentEntity actor)
        {
            var cmd = new MergeActorCommand()
            {
                Actor = actor
            };

            return cmd;
        }
    }
}
