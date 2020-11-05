using AutoMapper;
using Doctrina.Application.ActivityStates.Commands;
using Doctrina.Application.Common;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Models.Documents;
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
        private readonly IStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteActivityStatesHandler(IStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteActivityStatesCommand request, CancellationToken cancellationToken)
        {
            Guid storeId = _context.StoreId;

            string activityHash = request.ActivityId.ComputeHash();

            IQueryable<ActivityStateEntity> activities = _context.Documents
                .OfType<ActivityStateEntity>()
                .Where(x=> x.StoreId == storeId)
                .Where(x => x.Activity.Hash == activityHash)
                .Where(x => x.PersonaId == request.Persona.PersonaId);

            _context.Documents.RemoveRange(activities);

            await _context.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }
    }
}
