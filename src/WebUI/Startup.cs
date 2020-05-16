using Doctrina.Application;
using Doctrina.Application.Statements.Commands;
using Doctrina.Infrastructure;
using Doctrina.Persistence;
using Doctrina.WebUI.Data;
using Doctrina.WebUI.ExperienceApi;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;

namespace Doctrina.WebUI
{
    public class Startup
    {
        public readonly IConfiguration Configuration;
        public readonly IWebHostEnvironment Environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddInfrastructure(Configuration, Environment);
            services.AddPersistence(Configuration);
            services.AddApplication(Configuration);

            services.AddHealthChecks()
              .AddDbContextCheck<DoctrinaDbContext>();

            services.AddHttpContextAccessor();

            services.AddLearningRecordStore();

            services.AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateStatementsCommandValidator>())
                .AddExperienceApi();

            services.AddResponseCompression();

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //services.AddOpenApiDocument(configure =>
            //{
            //    configure.Title = "Doctrina LRS API";
            //});

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<RegistrationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseResponseCompression();

            app.UseSerilogRequestLogging();

            // Handle Lets Encrypt Route (before MVC processing!)
            app.UseRouter(wellKnown =>
            {
                wellKnown.MapGet(".well-known/acme-challenge/{id}", async (request, response, routeData) =>
                {
                    var id = routeData.Values["id"] as string;
                    var file = Path.Combine(env.WebRootPath, ".well-known", "acme-challenge", id);
                    await response.SendFileAsync(file);
                });
            });

            app.UseHealthChecks("/health");
            if (!Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }
            app.UseStaticFiles();

            //app.UseOpenApi();

            //app.UseSwaggerUi3(settings =>
            //{
            //    settings.Path = "/xapi";
            //    //settings.DocumentPath = "/api/specification.json";
            //});
            app.UseExperienceApiEndpoints();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=index}/{id?}");
                endpoints.MapControllers();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub().RequireAuthorization(new AuthorizeAttribute {
                    AuthenticationSchemes = "Cookies"
                });
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
