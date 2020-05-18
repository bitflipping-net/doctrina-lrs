using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityStates.Queries
{
    public class GetActivityStatesQueryHandler : IRequestHandler<GetActivityStatesQuery, ICollection<ActivityStateDocument>>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetActivityStatesQueryHandler(IDoctrinaDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ICollection<ActivityStateDocument>> Handle(GetActivityStatesQuery request, CancellationToken cancellationToken)
        {
            string activityHash = request.ActivityId.ComputeHash();

            AgentEntity agent = _mapper.Map<AgentEntity>(request.Agent);

            var query = _context.ActivityStates
                .AsNoTracking()
                .Where(x => x.Activity.Hash == activityHash)
                .Where(x => x.Agent.Hash == agent.Hash);

            if (request.Registration.HasValue)
            {
                query.Where(x => x.Registration == request.Registration);
            }

            var states = await query.ToListAsync(cancellationToken);

            return _mapper.Map<ICollection<ActivityStateDocument>>(states);
        }
    }
}
