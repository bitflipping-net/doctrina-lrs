using Doctrina.Application.Activities.Queries;
using Doctrina.Domain.Entities.Documents;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityProfiles.Commands
{
    public class DeleteActivityProfileHandler : IRequestHandler<DeleteActivityProfileCommand>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;

        public DeleteActivityProfileHandler(IDoctrinaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteActivityProfileCommand request, CancellationToken cancellationToken)
        {
            var activity = await _mediator.Send(GetActivityQuery.Create(request.ActivityId), cancellationToken);

            var profile = await _context.Documents
                .OfType<ActivityProfileEntity>()
                .GetProfileAsync(activity.ActivityId, request.ProfileId, request.Registration, cancellationToken);

            _context.Documents.Remove(profile);
            await _context.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }
    }
}
