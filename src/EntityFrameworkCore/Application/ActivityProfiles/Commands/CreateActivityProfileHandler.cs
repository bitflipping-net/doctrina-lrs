using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Domain.Models;
using Doctrina.Domain.Models.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;

namespace Doctrina.Application.ActivityProfiles.Commands
{
    public class CreateActivityProfileHandler : IRequestHandler<CreateActivityProfileCommand, ActivityProfileDocument>
    {
        private readonly IStoreDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateActivityProfileHandler(IStoreDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ActivityProfileDocument> Handle(CreateActivityProfileCommand request, CancellationToken cancellationToken)
        {
            ActivityModel activity = await _mediator.Send(UpsertActivityCommand.Create(request.ActivityId));

            var profile = new ActivityProfileModel(request.Content, request.ContentType)
            {
                StoreId = _context.StoreId,
                Key = request.ProfileId,
                ActivityId = activity.ActivityId,
                RegistrationId = request.RegistrationId,
            };

            await _context.Documents.AddAsync(profile, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ActivityProfileDocument>(profile);
        }
    }
}
