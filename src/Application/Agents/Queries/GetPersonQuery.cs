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

namespace Doctrina.Application.Agents.Queries
{
    public class GetPersonQuery : IRequest<Person>
    {
        public Agent Agent { get; set; }

        public class Handler : IRequestHandler<GetPersonQuery, Person>
        {
            private readonly IDoctrinaDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public Handler(IDoctrinaDbContext context, IMapper mapper, IMediator mediator)
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

        public static GetPersonQuery Create(Agent agent)
        {
            return new GetPersonQuery()
            {
                Agent = agent
            };
        }
    }
}
