using AutoMapper;
using Doctrina.Domain.Models;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IActivity = Doctrina.Domain.Models.Interfaces.IActivity;

namespace Doctrina.Application.Activities.Commands
{
    public class UpsertActivityCommandHandler : IRequestHandler<UpsertActivityCommand, IActivity>
    {
        private readonly IStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpsertActivityCommandHandler(IStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActivity> Handle(UpsertActivityCommand request, CancellationToken cancellationToken)
        {
            ActivityModel model = _mapper.Map<ActivityModel>(request.Activity);

            ActivityModel currentModel = await _context.Activities
                .Include(ac => ac.Definition)
                .FirstOrDefaultAsync(x => x.Hash == model.Hash, cancellationToken);

            if (currentModel != null)
            {
                if (model.Definition != null)
                {
                    if (currentModel.Definition == null)
                    {
                        currentModel.Definition = new ActivityDefinitionEntity();
                    }

                    _mapper.Map(model.Definition, currentModel.Definition);

                    await _context.SaveChangesAsync(cancellationToken);
                }
                return currentModel;
            }

            await _context.Activities.AddAsync(model, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return model;
        }
    }
}
