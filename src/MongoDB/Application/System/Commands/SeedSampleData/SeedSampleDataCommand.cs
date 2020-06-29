using Doctrina.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.System.Commands.SeedSampleData
{
    public class SeedSampleDataCommand : IRequest<Unit>
    {
    }

    public class SeedSampleDataCommandHandler : IRequestHandler<SeedSampleDataCommand, Unit>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IUserManager _userManager;

        public SeedSampleDataCommandHandler(IDoctrinaDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
        {
            var seeder = new SampleDataSeeder(_context, _userManager);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}