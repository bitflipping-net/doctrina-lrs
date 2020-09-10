using Doctrina.Infrastructure.Identity;
using Doctrina.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Doctrina.WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var config = services.GetRequiredService<IConfiguration>();

                Log.Logger = new LoggerConfiguration()
                   .ReadFrom.Configuration(config)
                   .MinimumLevel.Debug()
                   .Enrich.FromLogContext()
                   .CreateLogger();

                try
                {
                    var doctrinaContext = services.GetRequiredService<IDoctrinaDbProvider>();
                    doctrinaContext.Migrate();

                    var identityContext = services.GetRequiredService<DoctrinaAuthorizationDbContext>();
                    identityContext.Database.Migrate();

                    await host.RunAsync();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or initializing the database.");
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {

                   webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                   {
                       var env = hostingContext.HostingEnvironment;
                       config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                             .AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true)
                             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                       if (env.IsDevelopment())
                       {
                           var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                           if (appAssembly != null)
                           {
                               config.AddUserSecrets(appAssembly, optional: true);
                           }
                       }

                       config.AddEnvironmentVariables();

                       if (args != null)
                       {
                           config.AddCommandLine(args);
                       }
                   });
                   // webBuilder.ConfigureLogging((context, logging) =>
                   //  {
                   //      // Clear our default providers
                   //      logging.ClearProviders();
                   //  });

                   webBuilder.UseSerilog();

                   webBuilder.UseStartup<Startup>();
               });
        }
    }
}
