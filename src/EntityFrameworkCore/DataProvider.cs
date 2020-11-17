using Doctrina.Application.Common.Caching;
using Doctrina.Application.Identity;
using Doctrina.ExperienceApi.Resources;
using Doctrina.ExperienceApi.Server.Resources;
using Doctrina.Infrastructure.Interfaces;
using Doctrina.Persistence;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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

            services.AddTransient<IAboutResource, AboutResource>();
            services.AddTransient<IActivityProfileResource, ActivityProfileResource>();
            services.AddTransient<IAgentProfileResource, AgentProfileResource>();
            services.AddTransient<IActivityResource, ActivityResource>();
            services.AddTransient<IStateResource, ActivityStateResource>();
            services.AddTransient<IAgentResource, AgentResource>();
            services.AddTransient<IStatementResource, StatementResource>();

            services.AddScoped<IClientContext, ClientContext>();

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
