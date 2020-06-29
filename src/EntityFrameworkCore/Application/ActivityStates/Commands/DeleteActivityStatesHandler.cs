using AutoMapper;
using Doctrina.Application.ActivityStates.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using MediatR;
using System.Linq;
using Doctrina.Persistence.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityStates
{
    public class DeleteActivityStatesHandler : IRequestHandler<DeleteActivityStatesCommand>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;

        public DeleteActivityStatesHandler(IDoctrinaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteActivityStatesCommand request, CancellationToken cancellationToken)
        {
            string activityHash = request.ActivityId.ComputeHash();
            var activities = _context.ActivityStates.Where(x => x.Activity.Hash == activityHash)
                .Where(x => x.Agent.AgentId == request.AgentId);

            _context.ActivityStates.RemoveRange(activities);

            await _context.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }
    }
}
