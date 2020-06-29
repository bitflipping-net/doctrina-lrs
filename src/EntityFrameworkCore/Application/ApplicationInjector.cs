using Doctrina.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Doctrina.Persistence;
using Doctrina.Application.Common.Caching;
using MediatR;

namespace Doctrina.Application
{
    public class ApplicationInjector : IApplicationInjector
    {
        public IServiceCollection AddApplication(IServiceCollection services, IConfiguration config)
        {
            // IoC on application injector
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
