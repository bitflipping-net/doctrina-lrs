using System.Reflection;
using Doctrina.Application.Common.Caching;
using Doctrina.ExperienceApi;
using Doctrina.ExperienceApi.Server.Services;
using Doctrina.ExperienceApi.Services;
using Doctrina.Infrastructure.Interfaces;
using Doctrina.Persistence;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doctrina.Application
{
    public class DataProvider : IDataProviderInjector
    {
        public IServiceCollection AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DoctrinaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DoctrinaDatabase")));

            services.AddScoped<IDoctrinaDbContext>(provider => provider.GetService<DoctrinaDbContext>());

            services.AddScoped(typeof(IDoctrinaDbProvider), typeof(EntityFrameworkCoreDbProvider));

            return services;
        }

        public IServiceCollection AddApplication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IActivityProfileService, ActivityProfileService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IActivityStateService, ActivityStateService>();
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IStatementService, StatementService>();

            // IoC on application injector
            services.Scan(scan => scan.FromAssembliesOf(typeof(DataProvider))
                .AddClasses(classes => classes.AssignableTo(typeof(ICache<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICacheInvalidator<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            );

            return services;
        }
    }
}
