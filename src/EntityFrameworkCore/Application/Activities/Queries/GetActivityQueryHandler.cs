using AutoMapper;
using Doctrina.Application.Activities.Queries;
using Doctrina.Domain.Models;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Activities
{
    public class GetActivityQueryHandler : IRequestHandler<GetActivityQuery, ActivityModel>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;

        public GetActivityQueryHandler(IDoctrinaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ActivityModel> Handle(GetActivityQuery request, CancellationToken cancellationToken)
        {
            string activityHash = request.ActivityId.ComputeHash();
            ActivityModel activity = await _context.Activities
                .AsNoTracking()
                .Where(x => x.Hash == activityHash)
                .Include(ac => ac.Definition)
                .FirstOrDefaultAsync(cancellationToken);

            return activity;
        }
    }
}
