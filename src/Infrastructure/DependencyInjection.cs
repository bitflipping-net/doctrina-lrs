using Doctrina.Application.Common.Interfaces;
using Doctrina.Common;
using Doctrina.Infrastructure.Identity;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;

namespace Doctrina.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {

            services.AddScoped<IUserManager, UserManagerService>();
            services.AddTransient<IDateTime, MachineDateTime>();

            services.ConfigureSelfHostedEnvironment(configuration, environment);

            return services;
        }

        private static IServiceCollection ConfigureSelfHostedEnvironment(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            string connectionString = configuration.GetConnectionString("DoctrinaAuthorizationDatabase");
            var migrationsAssemblyName = typeof(DependencyInjection).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<DoctrinaAuthorizationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDefaultIdentity<DoctrinaUser>()
                    .AddEntityFrameworkStores<DoctrinaAuthorizationDbContext>();

            if (environment.IsEnvironment("Test"))
            {
                services.AddIdentityServer()
                    .AddApiAuthorization<DoctrinaUser, DoctrinaAuthorizationDbContext>(options =>
                    {
                        options.Clients.Add(new Client
                        {
                            ClientId = "Doctrina.IntegrationTests",
                            ClientSecrets = { new Secret("secret".Sha256()) },
                            AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                            AllowedScopes = { "Doctrina.WebUIAPI", "openid", "profile" }
                        });
                    }).AddTestUsers(new List<TestUser>
                    {
                        new TestUser
                        {
                            SubjectId = "f26da293-02fb-4c90-be75-e4aa51e0bb17",
                            Username = "rm@doctrina.net",
                            Password = "Doctrina1!",
                            Claims = new List<Claim>
                            {
                                new Claim(JwtClaimTypes.Email, "rm@doctrina.net")
                            }
                        }
                    });
            }
            else
            {
                services.AddIdentityServer()
                    // this adds the operational data from DB (codes, tokens, consents)
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = builder =>
                            builder.UseSqlServer(connectionString,
                                sql => sql.MigrationsAssembly(migrationsAssemblyName));

                        // this enables automatic token cleanup. this is optional.
                        options.EnableTokenCleanup = true;
                        options.TokenCleanupInterval = 30; // interval in seconds
                    })
                    .AddApiAuthorization<DoctrinaUser, DoctrinaAuthorizationDbContext>();
            }

            services.AddAuthentication(auth => {
                auth.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                auth.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options => {
                configuration.GetSection("OpenIdConnect").Bind(options);
            })
            .AddIdentityServerJwt();


            return services;
        }
    }
}
