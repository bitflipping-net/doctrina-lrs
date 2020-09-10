using AutoMapper;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Agents.Queries
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, Person>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetPersonQueryHandler(IDoctrinaDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Person> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            var person = new Person();
            person.Add(request.Agent);

            var agentEntity = await _mediator.Send(GetAgentQuery.Create(request.Agent), cancellationToken);

            if (agentEntity != null)
            {
                person.Add(_mapper.Map<Agent>(agentEntity));
            }

            return person;
        }
    }
}