using AutoMapper;
using Doctrina.Application.ActivityStates.Commands;
using Doctrina.Application.Common;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityStates
{
    public class DeleteActivityStatesHandler : IRequestHandler<DeleteActivityStatesCommand>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStoreContext _storeContext;

        public DeleteActivityStatesHandler(IDoctrinaDbContext context, IMapper mapper, IStoreContext storeContext)
        {
            _context = context;
            _mapper = mapper;
            _storeContext = storeContext;
        }

        public async Task<Unit> Handle(DeleteActivityStatesCommand request, CancellationToken cancellationToken)
        {
            Guid storeId = _storeContext.GetStoreId();
            string activityHash = request.ActivityId.ComputeHash();
            var activities = _context.ActivityStates
                .Where(x=> x.StoreId == storeId)
                .Where(x => x.Activity.Hash == activityHash)
                .Where(x => x.PersonaIdentifier.Id == request.IFI.Id);

            _context.ActivityStates.RemoveRange(activities);

            await _context.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }
    }
}
