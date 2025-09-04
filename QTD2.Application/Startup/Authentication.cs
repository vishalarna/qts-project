using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Sustainsys.Saml2.Metadata;
using Sustainsys.Saml2;
using System.Security.Cryptography.X509Certificates;
using QTD2.Domain.Entities.Authentication;
using QTD2.Data;
using System.Linq;
using Sustainsys.Saml2.Saml2P;
using System.Collections.Generic;
using System.Configuration;
using QTD2.Infrastructure.QTD2Auth.Settings;

namespace QTD2.Application.Startup
{
    public static class Authentication
    {
        public static string TwoFactoryAuthenticationTokenProvider
        {
            get { return "TwoFactoryAuthenticationTokenProvider"; }
        }

        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 14;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365 * 100);
                options.Lockout.MaxFailedAccessAttempts = 6;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.AddScoped<UserManager<Domain.Entities.Authentication.AppUser>>();

            services.AddDefaultIdentity<Domain.Entities.Authentication.AppUser>()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<Data.QTDAuthenticationContext>()
                    .AddTokenProvider("QTSAuth", typeof(DataProtectorTokenProvider<Domain.Entities.Authentication.AppUser>))
                    .AddTokenProvider<CustomMfaTokenProvider<AppUser>>("CustomMfaTokenProvider");

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(int.Parse(configuration["ResetPasswordSettings:TokenLifetimeMinutes"]));
            });
        }

        public static void ConfigureJWTAuthentication_Client(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie("External")
            .AddSaml2(options =>
            {
                var entityId = configuration["SamlSettings:EntityId"];
                var returnUrl = configuration["SamlSettings:ReturnUrl"];
                if(entityId ==null || returnUrl== null)
                {
                    return;
                }
                options.SPOptions.EntityId = new EntityId(entityId);
                options.SPOptions.ReturnUrl = new Uri(returnUrl);
                options.SignInScheme = "External";
                options.SignOutScheme = "External";
                options.Notifications.AuthenticationRequestCreated = AuthenticationRequestCreated;

                using (var serviceProvider = services.BuildServiceProvider())
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<QTDAuthenticationContext>();
                        var activeProviders = dbContext.IdentityProviders.Where(ip => ip.Active).ToList();

                        foreach (var provider in activeProviders)
                        {
                            try
                            {
                                switch (provider.Type.ToUpperInvariant())
                                {
                                    case "SAML":
                                        if (provider is SamlProvider samlProvider)
                                        {
                                            options.IdentityProviders.Add(new Sustainsys.Saml2.IdentityProvider(
                                                new EntityId(samlProvider.EntityIDUrl), options.SPOptions)
                                            {
                                                MetadataLocation = samlProvider.MetaDataUrl,
                                                LoadMetadata = true
                                            });
                                        }
                                        break;
                                    case "OAUTH":
                                        break;
                                    default:
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                            }
                        }
                    }
                }
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:TokenSecretKey").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    ClockSkew = TimeSpan.Zero,              
                };
            });
        }

        private static void AuthenticationRequestCreated(Saml2AuthenticationRequest request, Sustainsys.Saml2.IdentityProvider idp, IDictionary<string, string> dict)
        {
            request.ForceAuthentication = false;
        }

        public static void AddSharedCookiedDataProtection(this IServiceCollection services, IConfiguration configuration)
        {
            var contentRoot = configuration.GetValue<string>("Authentication:KeyRootPath");
            var dir = new System.IO.DirectoryInfo(contentRoot + @"\keys\");

            services.AddDataProtection().PersistKeysToFileSystem(dir).SetApplicationName("QTDSuite");
        }
    }
}
