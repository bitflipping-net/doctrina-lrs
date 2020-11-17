using System;
using System.Linq;
using Doctrina.Application.Extensions;
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

            if (!dbContext.Clients.Any())
            {
                dbContext.Clients.Add(new Domain.Entities.Client()
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
                    Scopes = new string[] { "all" },
                    Enabled = true
                });
                dbContext.SaveChanges();
            }
        }
    }
}