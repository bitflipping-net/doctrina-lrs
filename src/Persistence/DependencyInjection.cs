using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doctrina.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DoctrinaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DoctrinaDatabase")));

            services.AddScoped<IDoctrinaDbContext>(provider => provider.GetService<DoctrinaDbContext>());

            return services;
        }
    }
}