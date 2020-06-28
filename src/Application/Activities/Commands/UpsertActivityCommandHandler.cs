using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Interfaces;
using Doctrina.Domain.Entities.OwnedTypes;
using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using IActivity = Doctrina.Domain.Entities.Interfaces.IActivity;

namespace Doctrina.Application.Activities.Commands
{
    public class UpsertActivityCommandHandler : IRequestHandler<UpsertActivityCommand, IActivity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMapper _mapper;

        public UpsertActivityCommandHandler(IDoctrinaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActivity> Handle(UpsertActivityCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ActivityEntity>(request.Activity);

            var current = await _context.Activities
                .Include(ac => ac.Definition)
                .FirstOrDefaultAsync(x => x.Hash == entity.Hash, cancellationToken);

            if(current != null)
            {
                if(entity.Definition != null)
                {
                    if(current.Definition == null)
                    {
                        current.Definition = new ActivityDefinitionEntity();
                    }
                    _mapper.Map(entity.Definition, current.Definition);
                }
                return current;
            }

            entity.ActivityId = Guid.NewGuid();
            _context.Activities.Add(entity);

            return entity;
        }
    }
}
