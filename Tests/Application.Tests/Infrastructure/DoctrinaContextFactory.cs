using Doctrina.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace Doctrina.Application.Tests.Infrastructure
{
    public class DoctrinaContextFactory
    {
        public static DoctrinaDbContext Create()
        {
            var options = new DbContextOptionsBuilder<DoctrinaDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DoctrinaDbContext(options);

            context.Database.EnsureCreated();

            //context.Statements.AddRange(new[] {
               
            //});

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
