using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace Doctrina.Application.Tests.Infrastructure
{
    public static class DoctrinaContextFactory
    {
        public static DoctrinaDbContext Create()
        {
            var options = new DbContextOptionsBuilder<DoctrinaDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DoctrinaDbContext(options);

            context.Database.EnsureCreated();

            // TODO: Parse statements from file, and map to entities.
            //var collection = new StatementCollection();

            context.SaveChanges();

            return context;
        }

        public static void Destroy(DoctrinaDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
