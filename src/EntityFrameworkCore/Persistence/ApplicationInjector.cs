using Doctrina.Application;
using Doctrina.EntityFrameworkCore.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doctrina.Persistence
{
    public class ApplicationInjector : IApplicationInjector
    {
        public IServiceCollection AddApplication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DoctrinaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DoctrinaDatabase")));

            services.AddScoped<IDoctrinaDbContext>(provider => provider.GetService<DoctrinaDbContext>());

            return services;
        }
    }
}
