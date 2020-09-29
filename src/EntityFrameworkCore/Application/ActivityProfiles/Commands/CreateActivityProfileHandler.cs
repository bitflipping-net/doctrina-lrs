using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityProfiles.Commands
{
    public class CreateActivityProfileHandler : IRequestHandler<CreateActivityProfileCommand, ActivityProfileDocument>
    {
        private readonly IStoreDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateActivityProfileHandler(IStoreDbContext context, IStoreHttpContext storeContext, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ActivityProfileDocument> Handle(CreateActivityProfileCommand request, CancellationToken cancellationToken)
        {
            var activity = await _mediator.Send(UpsertActivityCommand.Create(request.ActivityId));

            var profile = new ActivityProfileEntity(request.Content, request.ContentType)
            {
                Key = request.ProfileId,
                Activity = activity as ActivityModel,
                RegistrationId = request.Registration,
                StoreId = _context.StoreId,
            };

            await _context.Documents.AddAsync(profile, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            //return profile;
            return _mapper.Map<ActivityProfileDocument>(profile);
        }
    }
}
