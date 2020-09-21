using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class DeleteActivityStateHandler : IRequestHandler<DeleteActivityStateCommand>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IStoreContext _storeContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteActivityStateHandler(IDoctrinaDbContext context, IStoreContext storeContext, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _storeContext = storeContext;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteActivityStateCommand request, CancellationToken cancellationToken)
        {
            Guid storeId = _storeContext.GetStoreId();
            string activityHash = request.ActivityId.ComputeHash();
            var activity = await _context.ActivityStates
                .SingleOrDefaultAsync(x => 
                    x.StoreId == storeId
                    && x.Key == request.StateId
                    && x.Activity.Hash == activityHash
                    && (!request.Registration.HasValue || x.RegistrationId == request.Registration)
                    && x.PersonaIdentifier.Id == request.IFI.Id,
                    cancellationToken);

            if (activity != null)
            {
                _context.ActivityStates.Remove(activity);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return await Unit.Task;
        }
    }
}
