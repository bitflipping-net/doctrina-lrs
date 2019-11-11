using AutoMapper;
using Doctrina.Application.Common.Behaviours;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Doctrina.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddSingleton<IDoctrinaAppContext, DoctrinaAppContext>();

            return services;
        }
    }
}
