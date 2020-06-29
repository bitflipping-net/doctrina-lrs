using Doctrina.Application;
using Doctrina.Application.Common.Caching;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doctrina.MongoDB.Application
{
    public class ApplicationInjector : IApplicationInjector
    {
        public IServiceCollection AddApplication(IServiceCollection services, IConfiguration config)
        {
            services.Scan(scan => scan.FromAssembliesOf(typeof(ApplicationInjector))
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
