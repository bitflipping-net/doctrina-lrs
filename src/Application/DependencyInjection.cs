using AutoMapper;
using Doctrina.Application.Common.Behaviours;
using Doctrina.Application.Common.Caching;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Infrastructure;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Doctrina.Application.Agents.Queries;

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

            services.AddSingleton<IDoctrinaAppContext, DoctrinaAppContext>();

            // Register ICache and ICacheInvalidators using Scrutor
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(DependencyInjection))
                    .AddClasses(classes => classes.AssignableTo(typeof(ICache<,>)))
                        .AsImplementedInterfaces()
                    .AddClasses(classes => classes.AssignableTo(typeof(ICacheInvalidator<>)))
                        .AsImplementedInterfaces()
            );

            services.AddMemoryCache();

            if(config["DistCache:Type"] == "Redis")
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = config["DistCache:Configuration"];
                    options.InstanceName = config["DistCache:InstanceName"];
                });
            }
            else
            {
                services.AddDistributedMemoryCache();
            }

            return services;
        }
    }
}
