using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Doctrina.Domain.Models.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Doctrina.Application.ActivityStates.Commands
{
    public class DeleteActivityStateHandler : IRequestHandler<DeleteActivityStateCommand>
    {
        private readonly IStoreDbContext _storeContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteActivityStateHandler(IStoreDbContext storeContext, IMapper mapper, IMediator mediator)
        {
            _storeContext = storeContext;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteActivityStateCommand request, CancellationToken cancellationToken)
        {
            string activityHash = request.ActivityId.ComputeHash();
            var activity = await _storeContext.Documents.OfType<ActivityStateEntity>()
                .SingleOrDefaultAsync(x => 
                    x.StoreId == _storeContext.StoreId
                    && x.Key == request.StateId
                    && x.Activity.Hash == activityHash
                    && (!request.Registration.HasValue || x.RegistrationId == request.Registration)
                    && x.Persona.PersonaId == request.Persona.PersonaId,
                    cancellationToken);

            if (activity != null)
            {
                _storeContext.Documents.Remove(activity);
                await _storeContext.SaveChangesAsync(cancellationToken);
            }

            return await Unit.Task;
        }
    }
}
