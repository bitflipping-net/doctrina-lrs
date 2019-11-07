using Doctrina.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Doctrina.Persistence
{
    public class DoctrinaDbContextFactory : DesignTimeDbContextFactoryBase<DoctrinaDbContext>
    {
        protected override DoctrinaDbContext CreateNewInstance(DbContextOptions<DoctrinaDbContext> options)
        {
            return new DoctrinaDbContext(options);
        }
    }
}
