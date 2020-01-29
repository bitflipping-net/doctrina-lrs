using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Agents.Queries
{
    public class GetPersonCommand : IRequest<Person>
    {
        public Agent Agent { get; set; }

        public class Handler : IRequestHandler<GetPersonCommand, Person>
        {
            private readonly IDoctrinaDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IDoctrinaDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Person> Handle(GetPersonCommand request, CancellationToken cancellationToken)
            {
                var person = new Person();
                person.Add(request.Agent);

                var agentEntity = _mapper.Map<AgentEntity>(request.Agent);

                agentEntity = await _context.Agents.FirstOrDefaultAsync(x => 
                    x.ObjectType == EntityObjectType.Agent 
                    && x.Hash == agentEntity.Hash, 
                    cancellationToken);

                if (agentEntity != null)
                {
                    person.Add(_mapper.Map<Agent>(agentEntity));
                }

                return person;
            }
        }

        public static GetPersonCommand Create(Agent agent)
        {
            return new GetPersonCommand()
            {
                Agent = agent
            };
        }
    }
}
