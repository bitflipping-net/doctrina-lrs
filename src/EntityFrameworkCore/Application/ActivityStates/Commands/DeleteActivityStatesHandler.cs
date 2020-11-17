using AutoMapper;
using Doctrina.Application.ActivityStates.Commands;
using Doctrina.Application.Agents.Queries;
using Doctrina.Domain.Entities.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityStates
{
    public class DeleteActivityStatesHandler : IRequestHandler<DeleteActivityStatesCommand>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public DeleteActivityStatesHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            this.mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteActivityStatesCommand request, CancellationToken cancellationToken)
        {
            var agent = await mediator.Send(GetAgentQuery.Create(request.Agent));

            string activityHash = request.ActivityId.ComputeHash();
            var activities = _context.Documents
                .OfType<ActivityStateEntity>()
                .Where(x => x.Activity.Hash == activityHash)
                .Where(x => x.Agent.AgentId == agent.AgentId);

            _context.Documents.RemoveRange(activities);

            await _context.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }
    }
}
