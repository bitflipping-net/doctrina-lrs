using Doctrina.Application.Tests.Common;
using Doctrina.Domain.Entities;
using Doctrina.Persistence;
using Doctrina.ExperienceApi.Data;
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

            var activityId = new Iri("http://www.example.com/activityId/hashset");
            context.Activities.Add(new ActivityEntity(){
                Id = activityId.ToString(),
                Hash = activityId.ComputeHash()
            });

            context.Agents.AddRange(new AgentEntity[]{
                AgentsTestFixture.JamesCampbell(),
                AgentsTestFixture.JosephinaCampbell()
            });

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
