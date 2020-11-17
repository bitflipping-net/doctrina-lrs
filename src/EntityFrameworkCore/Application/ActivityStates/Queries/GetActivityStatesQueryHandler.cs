using AutoMapper;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityStates.Queries
{
    public class GetActivityStatesQueryHandler : IRequestHandler<GetActivityStatesQuery, ICollection<ActivityStateEntity>>
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

        public async Task<ICollection<ActivityStateEntity>> Handle(GetActivityStatesQuery request, CancellationToken cancellationToken)
        {
            string activityHash = request.ActivityId.ComputeHash();
            Guid agentId = request.AgentId;

            var query = _context.Documents
                .AsNoTracking()
                .OfType<ActivityStateEntity>()
                .Where(x => x.Activity.Hash == activityHash)
                .Where(x => x.Agent.AgentId == agentId);

            if (request.Registration.HasValue)
            {
                query.Where(x => x.RegistrationId == request.Registration);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
