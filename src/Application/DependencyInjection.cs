using AutoMapper;
using Doctrina.Application.Common.Behaviours;
using Doctrina.Application.Common.Caching;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Doctrina.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Configure MediatR Pipeline with cache invalidation and cached request behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheInvalidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddSingleton(typeof(IDoctrinaAppContext), typeof(DoctrinaAppContext));

            services.Scan(scan => scan.FromApplicationDependencies()
                .AddClasses(clases => clases.AssignableTo(typeof(IDataProviderInjector)))
                    .AsImplementedInterfaces()
            );
            var provider = services.BuildServiceProvider();
            var injector = provider.GetService<IDataProviderInjector>();
            injector.AddPersistence(services, config);
            injector.AddApplication(services, config);

            // Register ICache and ICacheInvalidators using Scrutor
            // services.Scan(scan => scan
            //     .FromAssembliesOf(typeof(DependencyInjection))
            //         .AddClasses(classes => classes.AssignableTo(typeof(ICache<,>)))
            //             .AsImplementedInterfaces()
            //         .AddClasses(classes => classes.AssignableTo(typeof(ICacheInvalidator<>)))
            //             .AsImplementedInterfaces()
            // );

            // services.AddMemoryCache();

            // if(config["DistCache:Type"] == "Redis")
            // {
            //     services.AddStackExchangeRedisCache(options =>
            //     {
            //         options.Configuration = config["DistCache:Configuration"];
            //         options.InstanceName = config["DistCache:InstanceName"];
            //     });
            // }
            // else
            // {
            //     services.AddDistributedMemoryCache();
            // }

            return services;
        }
    }
}
