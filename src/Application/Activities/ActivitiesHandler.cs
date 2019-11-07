using AutoMapper;
using Doctrina.Application.Activities.Queries;
using Doctrina.Application.Common.Interfaces;
using Doctrina.ExperienceApi.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.Activities
{
    public class ActivitiesHandler :
        IRequestHandler<GetActivityQuery, Activity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;

        public ActivitiesHandler(IDoctrinaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Activity> Handle(GetActivityQuery request, CancellationToken cancellationToken)
        {
            string activityHash = request.ActivityId.ComputeHash();
            var activity = await _context.Activities.FirstOrDefaultAsync(x => x.Hash == activityHash);
            return _mapper.Map<Activity>(activity);
        }
    }
}
