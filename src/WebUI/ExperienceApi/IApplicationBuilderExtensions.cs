using Doctrina.WebUI.ExperienceApi.Routing;
using Microsoft.AspNetCore.Builder;

namespace Doctrina.WebUI.ExperienceApi
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Maps required middleware for Experience API
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExperienceApiEndpoints(this IApplicationBuilder builder)
        {
            builder.MapWhen(context => context.Request.Path.StartsWithSegments("/xapi"), experienceApi =>
            {
                experienceApi.UseMiddleware<ApiExceptionMiddleware>();

                experienceApi.UseMiddleware<AlternateRequestMiddleware>();

                experienceApi.UseRequestLocalization();

                experienceApi.UseRouting();

                experienceApi.UseAuthentication();
                experienceApi.UseAuthorization();

                experienceApi.UseMiddleware<ConsistentThroughMiddleware>();
                experienceApi.UseMiddleware<UnrecognizedParametersMiddleware>();

                experienceApi.UseEndpoints(routes =>
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
