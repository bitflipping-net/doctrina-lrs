using Doctrina.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Doctrina.Persistence.Infrastructure
{
    public class EntityFrameworkCoreDbProvider : IDoctrinaDbProvider
    {
        private readonly DoctrinaDbContext dbContext;

        public EntityFrameworkCoreDbProvider(DoctrinaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Migrate()
        {
            dbContext.Database.Migrate();
        }
    }
}