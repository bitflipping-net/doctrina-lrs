using Doctrina.Application.Common.Interfaces;
using Doctrina.WebUI.ExperienceApi.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Doctrina.WebUI.ExperienceApi
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddLearningRecordStore(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IAuthorityContext, AuthorityContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ExperienceApiAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ExperienceApiAuthenticationOptions.DefaultScheme;
            })
            .AddExperienceApiAuthentication(options => { });

            // services.AddAuthorization();

            return services;
        }
    }
}
