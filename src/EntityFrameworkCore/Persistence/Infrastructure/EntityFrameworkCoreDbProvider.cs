using System;
using System.Collections.Generic;
using System.Linq;
using Doctrina.Application.Extensions;
using Doctrina.Domain.Entities;
using Doctrina.ExperienceApi.Data;
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

        public void Seed()
        {
            if (!dbContext.Clients.Any())
            {
                var client = new Domain.Entities.Client()
                {
                    API = "admin@example.com:zKR4gkYNHP5tvH".ToBasicAuth(),
                    Authority = new Agent()
                    {
                        Account = new Account()
                        {
                            Name = "Sample Client",
                            HomePage = new Uri("https://bitflipping.net")
                        }
                    }.ToJson(),
                    Name = "Sample Client",
                    Scopes = new List<string> { "all" },
                    Enabled = true,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                };
                dbContext.Clients.Add(client);

                dbContext.SaveChanges();
            }
        }
    }
}