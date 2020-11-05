using System.Reflection;
using Doctrina.Application.Common;
using Doctrina.Application.Common.Caching;
using Doctrina.Infrastructure.Interfaces;
using Doctrina.Persistence;
using Doctrina.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

            services.AddScoped<IClientHttpContext, ClientHttpContext>();

            services.AddScoped<StoreDbContext>().AddDbContext<StoreDbContext>(options =>
            {
                //services.AddScoped<IInterceptor, StoreColumnIntercepter>();
                options.UseInternalServiceProvider(services.BuildServiceProvider());
                options.UseSqlServer(configuration.GetConnectionString("DoctrinaDatabase"));
            });

            services.AddScoped<IStoreDbContext>(provider => provider.GetService<StoreDbContext>());

            services.AddScoped(typeof(IDoctrinaDbProvider), typeof(EntityFrameworkCoreDbProvider));

            return services;
        }

        public IServiceCollection AddApplication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

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
