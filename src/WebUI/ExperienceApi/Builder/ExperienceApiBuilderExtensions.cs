using Doctrina.WebUI.ExperienceApi.Authentication;
using Doctrina.WebUI.ExperienceApi.Controllers;
using Doctrina.WebUI.ExperienceApi.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Doctrina.WebUI.ExperienceApi.Builder
{
    public static class ExperienceApiBuilderExtensions
    {
        public static IMvcBuilder AddExperienceApi(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder
                .AddApplicationPart(typeof(StatementsController).Assembly)
                .AddControllersAsServices();

            return mvcBuilder;
        }

        public static IServiceCollection AddLearningRecordStore(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ExperienceApiAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ExperienceApiAuthenticationOptions.DefaultScheme;
            })
            .AddExperienceApiAuthentication(options => {
            });

            // services.AddAuthorization();

            return services;
        }

        /// <summary>
        /// Maps required middleware for Experience API
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExperienceApiEndpoints(this IApplicationBuilder builder)
        {
            builder.MapWhen(context => context.Request.Path.StartsWithSegments("/xapi"), xapiBuilder =>
            {
                xapiBuilder.UseRequestLocalization();

                xapiBuilder.UseAuthentication();

                xapiBuilder.UseMiddleware<AlternateRequestMiddleware>();
                xapiBuilder.UseMiddleware<ConsistentThroughMiddleware>();
                xapiBuilder.UseMiddleware<UnrecognizedParametersMiddleware>();

                xapiBuilder.UseRouting();

                xapiBuilder.UseEndpoints(routes =>
                {
                    routes.MapControllerRoute(
                        name: "experience_api",
                        pattern: "xapi/{controller=About}");
                });

            });

            return builder;
        }
    }
}
