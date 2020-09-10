
using AutoMapper;
using Doctrina.Application.Infrastructure.AutoMapper;
using Doctrina.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace Doctrina.Application.Tests
{
    public class TestBase
    {
        public DoctrinaDbContext GetDbContext(bool useSqlLite = false)
        {
            var builder = new DbContextOptionsBuilder<DoctrinaDbContext>();
            if (useSqlLite)
            {
                builder.UseSqlite("DataSource=:memory:", x => { });
            }
            else
            {
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }

            var dbContext = new DoctrinaDbContext(builder.Options);
            if (useSqlLite)
            {
                // SQLite needs to open connection to the DB.
                // Not required for in-memory-database.
                dbContext.Database.OpenConnection();
            }

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        public Profile GetAutomapperProfile()
        {
            return new AutoMapperProfile();
        }
    }
}