using Doctrina.Application.Common;
using Doctrina.Application.Tests.Common;
using Doctrina.Domain.Models;
using Doctrina.ExperienceApi.Data;
using Doctrina.Persistence;
using Doctrina.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Rest;
using System;
using System.Linq;
using System.Text;

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
            

            context.Organisations.AddRange(new Organisation[] {
                new Organisation()
                {
                    Name = "Test Organisation",
                    Owner = Guid.NewGuid(),
                }
            });
            context.SaveChanges();

            Organisation organisation = context.Organisations.FirstOrDefault();

            context.Stores.AddRange(new Store[]
            {
                new Store()
                {
                    Name = "My first store",
                    OrganisationId = organisation.OrganisationId,
                }
            });
            context.SaveChanges();

            Store myStore = context.Stores.FirstOrDefault();

            context.Clients.AddRange(new Client[]
            {
                new Client()
                {
                    API = Convert.ToBase64String(Encoding.UTF8.GetBytes("USERNAME:PASSWORD")),
                    Enabled = true,
                    Name = "Enabled client",
                    Authority = new Agent()
                    {
                        Account = new Account
                        {
                            HomePage = new Uri("https://doctrina.net"),
                            Name = "TestClient"
                        }
                    }.ToJson(),
                    Scopes = new string[]
                    {
                        "all"
                    },
                    StoreId = myStore.StoreId
                }
            });

            context.SaveChanges();

            return context;
        }

        public static StoreDbContext CreateStoreContext(IDoctrinaDbContext doctrinaDbContext)
        {
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var clientHttpContext = new ClientHttpContext(doctrinaDbContext);
            Client client = doctrinaDbContext.Clients.FirstOrDefault();
            var result = clientHttpContext.AuthenticateAsync(client.API).GetAwaiter().GetResult();

            var storeContext = new StoreDbContext(options, clientHttpContext);

            storeContext.Database.EnsureCreated();

            var activityId = new Iri("http://www.example.com/activityId/hashset");
            storeContext.Activities.Add(new ActivityModel()
            {
                Iri = activityId.ToString(),
                Hash = activityId.ComputeHash()
            });

            storeContext.Personas.AddRange(new PersonaModel[]{
                new PersonaModel()
                {
                    ObjectType = Domain.Models.ObjectType.Agent,
                    Name = "Facebook persona",
                    StoreId = client.StoreId,
                    Key = InverseFunctionalIdentifier.Account,
                    Value = new Account()
                    {
                        HomePage = new Uri("https://facebook.com/"),
                        Name = "Doctrina .NET"
                    }.ToJson()
                },
                new PersonaModel()
                {
                    ObjectType = Domain.Models.ObjectType.Agent,
                    Name = "Hello Doctrina",
                    StoreId = client.StoreId,
                    Key = InverseFunctionalIdentifier.Mbox,
                    Value = "mailto:hello@doctrina.net"
                },
            });

            storeContext.Activities.Add(new ActivityModel()
            {
                Iri = activityId.ToString(),
                Hash = activityId.ComputeHash()
            });

            storeContext.SaveChanges();

            return storeContext;
        }

        public static void Destroy(DoctrinaDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
