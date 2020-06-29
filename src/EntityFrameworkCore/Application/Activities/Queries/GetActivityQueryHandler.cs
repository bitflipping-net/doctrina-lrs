using AutoMapper;
using Doctrina.Application.Activities.Queries;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Persistence.Infrastructure;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Activities
{
    public class GetActivityQueryHandler : IRequestHandler<GetActivityQuery, ActivityEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;

        public GetActivityQueryHandler(IDoctrinaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ActivityEntity> Handle(GetActivityQuery request, CancellationToken cancellationToken)
        {
            string activityHash = request.ActivityId.ComputeHash();
            ActivityEntity activity = await _context.Activities
                .AsNoTracking()
                .Where(x => x.Hash == activityHash)
                .Include(ac => ac.Definition)
                .FirstOrDefaultAsync(cancellationToken);

            return activity;
        }
    }
}
