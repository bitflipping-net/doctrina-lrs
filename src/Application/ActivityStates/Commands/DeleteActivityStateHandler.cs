using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            string activityHash = request.ActivityId.ComputeHash();
            var agent = _mapper.Map<AgentEntity>(request.Agent);
            var activity = await _context.ActivityStates
                .Where(x => x.StateId == request.StateId && x.Activity.Hash == activityHash &&
                (!request.Registration.HasValue || x.Registration == request.Registration))
                .Where(x => x.Agent.Hash == agent.Hash)
                .FirstOrDefaultAsync(cancellationToken);

            if (activity != null)
            {
                _context.ActivityStates.Remove(activity);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return await Unit.Task;
        }
    }
}
