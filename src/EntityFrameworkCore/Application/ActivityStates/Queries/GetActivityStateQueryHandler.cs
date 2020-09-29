using AutoMapper;
using Doctrina.Domain.Models.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityStates.Queries
{
    public class GetActivityStateQueryHandler : IRequestHandler<GetActivityStateQuery, ActivityStateDocument>
    {
        private readonly IStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetActivityStateQueryHandler(IStoreDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActivityStateDocument> Handle(GetActivityStateQuery request, CancellationToken cancellationToken)
        {
            string activityHash = request.ActivityId.ComputeHash();

            var query = _context.Documents
                .OfType<ActivityStateEntity>()
                .AsNoTracking()
                .Where(x=> x.StoreId == _context.StoreId)
                .Where(x => x.StateId == request.StateId)
                .Where(x => x.Activity.Hash == activityHash)
                .Where(x => x.PersonaId == request.Persona.PersonaId);

            if (request.RegistrationId.HasValue)
            {
                query.Where(x => x.RegistrationId == request.RegistrationId);
            }

            ActivityStateEntity state = await query.SingleOrDefaultAsync(cancellationToken);

            return _mapper.Map<ActivityStateDocument>(state);
        }
    }
}
