using Doctrina.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Doctrina.MongoDB.Persistence
{
    public class ApplicationInjector : IApplicationInjector
    {
        public IServiceCollection AddApplication(IServiceCollection services, IConfiguration config)
        {

            services.Configure<DoctrinaDatabaseSettings>(ac => config.GetSection(nameof(DoctrinaDatabaseSettings)));

            services.AddSingleton<IDoctrinaDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DoctrinaDatabaseSettings>>().Value);

            services.AddSingleton<DoctrinaDbContext>();
            services.AddScoped<IDoctrinaDbContext>(provider => provider.GetService<DoctrinaDbContext>());

            return services;
        }
    }
}