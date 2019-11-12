using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Doctrina.Application.Activities.Commands
{
    public class MergeActivityCommandHandler : IRequestHandler<MergeActivityCommand, IActivityEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;

        public MergeActivityCommandHandler(IDoctrinaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActivityEntity> Handle(MergeActivityCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ActivityEntity>(request.Activity);

            var current = await _context.Activities.FirstOrDefaultAsync(x => x.Hash == entity.Hash);
            if(current != null)
            {
                return current;
            }

            _context.Activities.Add(entity);

            return entity;
        }
    }
}
