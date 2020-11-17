using AutoMapper;
using Doctrina.Application.Agents.Queries;
using Doctrina.Domain.Entities.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class DeleteActivityStateHandler : IRequestHandler<DeleteActivityStateCommand>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteActivityStateHandler(IDoctrinaDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteActivityStateCommand request, CancellationToken cancellationToken)
        {
            var agent = await _mediator.Send(GetAgentQuery.Create(request.Agent));

            string activityHash = request.ActivityId.ComputeHash();
            var activity = await _context.Documents
                .OfType<ActivityStateEntity>()
                .Where(x => x.Key == request.StateId && x.Activity.Hash == activityHash &&
                (!request.Registration.HasValue || x.RegistrationId == request.Registration))
                .Where(x => x.Agent.AgentId == agent.AgentId)
                .FirstOrDefaultAsync(cancellationToken);

            if (activity != null)
            {
                _context.Documents.Remove(activity);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return await Unit.Task;
        }
    }
}
