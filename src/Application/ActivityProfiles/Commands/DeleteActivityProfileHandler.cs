using Doctrina.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityProfiles.Commands
{
    public class DeleteActivityProfileHandler : IRequestHandler<DeleteActivityProfileCommand>
    {
        private readonly IDoctrinaDbContext _context;

        public DeleteActivityProfileHandler(IDoctrinaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteActivityProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _context.ActivityProfiles.GetProfileAsync(request.ActivityId, request.ProfileId, request.Registration, cancellationToken);

            _context.ActivityProfiles.Remove(profile);
            await _context.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }
    }
}
