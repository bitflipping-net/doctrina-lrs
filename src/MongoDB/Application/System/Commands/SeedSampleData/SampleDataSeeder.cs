using Doctrina.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.System.Commands.SeedSampleData
{
    public class SampleDataSeeder
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IUserManager _userManager;

        public SampleDataSeeder(IDoctrinaDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken = default)
        {
            await SeedUsersAsync(cancellationToken);
        }

        private Task SeedUsersAsync(CancellationToken cancellationToken = default)
        {
            // TODO: Seed user
            return Task.CompletedTask;
        }
    }
}
