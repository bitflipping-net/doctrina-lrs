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
    public class GetActivityStateQueryHandler : IRequestHandler<GetActivityStateQuery, ActivityStateDocument>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetActivityStateQueryHandler(IDoctrinaDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActivityStateDocument> Handle(GetActivityStateQuery request, CancellationToken cancellationToken)
        {
            string activityHash = request.ActivityId.ComputeHash();

            AgentEntity agent = _mapper.Map<AgentEntity>(request.Agent);

            var query = _context.ActivityStates
                .Where(x => x.Activity.Hash == activityHash)
                .Where(x => x.Agent.Hash == agent.Hash);

            if (request.Registration.HasValue)
            {
                query.Where(x => x.Registration == request.Registration);
            }

            ActivityStateEntity state = await query.SingleOrDefaultAsync(cancellationToken);

            return _mapper.Map<ActivityStateDocument>(state);
        }
    }
}
