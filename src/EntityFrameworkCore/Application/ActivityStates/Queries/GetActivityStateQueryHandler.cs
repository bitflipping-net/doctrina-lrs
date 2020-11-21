using AutoMapper;
using Doctrina.Application.Agents.Queries;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityStates.Queries
{
    public class GetActivityStateQueryHandler : IRequestHandler<GetActivityStateQuery, ActivityStateEntity>
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

        public async Task<ActivityStateEntity> Handle(GetActivityStateQuery request, CancellationToken cancellationToken)
        {
            var agent = await _mediator.Send(GetAgentQuery.Create(request.Agent));

            string activityHash = request.ActivityId.ComputeHash();

            var query = _context.Documents
                .AsNoTracking()
                .OfType<ActivityStateEntity>()
                .Where(x => x.Key == request.StateId)
                .Where(x => x.Activity.Hash == activityHash)
                .Where(x => x.Agent.AgentId == agent.AgentId);

            if (request.Registration.HasValue)
            {
                query.Where(x => x.RegistrationId == request.Registration);
            }

            ActivityStateEntity state = await query.SingleOrDefaultAsync(cancellationToken);

            return state;
        }
    }
}
