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

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            await SeedUsersAsync(cancellationToken);
        }

        private async Task SeedUsersAsync(CancellationToken cancellationToken)
        {
            // TODO: Seed user
        }
    }
}
