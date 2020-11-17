using AutoMapper;
using Doctrina.Application.Activities.Queries;
using Doctrina.Application.Common.Exceptions;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Server.Exceptions;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityProfiles.Commands
{
    public class UpdateActivityProfileHandler : IRequestHandler<UpdateActivityProfileCommand>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateActivityProfileHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateActivityProfileCommand request, CancellationToken cancellationToken)
        {
            var activity = await _mediator.Send(GetActivityQuery.Create(request.ActivityId));

            if (activity == null)
            {
                throw new NotFoundException("No activity profiles for activity.");
            }

            ActivityProfileEntity profile = await _context.Documents
                .OfType<ActivityProfileEntity>()
                .GetProfileAsync(activity.ActivityId, request.ProfileId, request.Registration, cancellationToken);

            profile.UpdateDocument(request.Content, request.ContentType);

            _context.Documents.Update(profile);
            await _context.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }
    }
}
