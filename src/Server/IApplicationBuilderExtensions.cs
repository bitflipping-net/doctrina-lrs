using Doctrina.Server.Routing;
using Microsoft.AspNetCore.Builder;
using System;

namespace Doctrina.Server
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Maps required middlewares for the LRS Server to receive statements from clients
        /// </summary>
        public static IApplicationBuilder UseExperienceApiEndpoints(this IApplicationBuilder builder, Action<ApiEndpointOptions> options = null)
        {
            var defaultOptions = new ApiEndpointOptions();
            options.Invoke(defaultOptions);

            builder.MapWhen(context => context.Request.Path.StartsWithSegments(defaultOptions.Path), experienceApi =>
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
