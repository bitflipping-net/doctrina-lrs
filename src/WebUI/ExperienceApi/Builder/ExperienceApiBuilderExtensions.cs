using Doctrina.Application.Common.Interfaces;
using Doctrina.WebUI.ExperienceApi.Authentication;
using Doctrina.WebUI.ExperienceApi.Controllers;
using Doctrina.WebUI.ExperienceApi.Mvc.ModelBinding.Providers;
using Doctrina.WebUI.ExperienceApi.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Doctrina.WebUI.ExperienceApi.Builder
{
    public static class ExperienceApiBuilderExtensions
    {
        public static IMvcBuilder AddExperienceApi(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder
                .AddApplicationPart(typeof(StatementsController).Assembly)
                .AddControllersAsServices();

            mvcBuilder.AddMvcOptions(options =>
            {
                options.ModelBinderProviders.Insert(0, new IriModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new AgentModelBinderProvider());
            });

            return mvcBuilder;
        }

        public static IServiceCollection AddLearningRecordStore(this IServiceCollection services)
        {
            services.AddScoped<IAuthorityContext, AuthorityContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ExperienceApiAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ExperienceApiAuthenticationOptions.DefaultScheme;
            })
            .AddExperienceApiAuthentication(options => {});

            // services.AddAuthorization();

            return services;
        }

        public static IApplicationBuilder UseAlternateRequestSyntax(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<AlternateRequestMiddleware>();

            return builder;
        }

        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ApiExceptionMiddleware>();

            return builder;
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
