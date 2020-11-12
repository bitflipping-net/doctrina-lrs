using AutoMapper;
using Doctrina.Application.Activities.Commands;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityProfiles.Commands
{
    public class CreateActivityProfileHandler : IRequestHandler<CreateActivityProfileCommand, ActivityProfileEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;

        public CreateActivityProfileHandler(IDoctrinaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ActivityProfileEntity> Handle(CreateActivityProfileCommand request, CancellationToken cancellationToken)
        {
            var activity = await _mediator.Send(UpsertActivityCommand.Create(request.ActivityId));

            var profile = new ActivityProfileEntity(request.Content, request.ContentType)
            {
                ProfileId = request.ProfileId,
                ActivityId = activity.ActivityId,
                RegistrationId = request.Registration
            };

            _context.ActivityProfiles.Add(profile);
            await _context.SaveChangesAsync(cancellationToken);

            //return profile;
            return profile;
        }
    }
}
