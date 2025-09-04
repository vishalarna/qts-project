using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using QTD2.Application.Authorization;
using QTD2.Infrastructure.Jobs.Interfaces;
using QTD2.Application.Startup;
using QTD2.Application.Startup.Middleware.Extensions;
using QTD2.Application.Jobs.Startup;
using System.Linq;
using Sustainsys.Saml2.Metadata;
using Sustainsys.Saml2;

namespace QTD2.API.Authentication
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(config => { config.Filters.Add(new AuthorizeFilter(Policies.Authenticated)); })
                .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QTD2.API.Authentication", Version = "v1" });
            });

            services.AddHttpContextAccessor();

            services.AddSharedHttpClients(Configuration);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());

            services.AddDatabaseServices(Configuration);

            services.AddAuthenticationDatabase(Configuration);

            services.AddAuthenticationDataServices(Configuration);

            services.AddAuthenticationDomainServices(Configuration);

            services.AddInfrastructureServices(Configuration);

            services.AddAuthenticationApplicationServices(Configuration);

            services.AddAuthInfrastructureServices(Configuration);

            services.ConfigureIdentity(Configuration);

            services.ConfigureJWTAuthentication_Client(Configuration);

            services.AddLocalization(Configuration);

            services.RegisterSettings(Configuration);

            services.AddAuthorizationPolicies(Configuration);

            services.AddTransient<IJob, QTDAuthDatabaseSetup>();

            services.AddCors(options =>
            {
                options.AddPolicy("DisAllowAllExceptQtdSpa", builder =>
                    {
                        var qtdDomain = Configuration.GetSection("ApplicationSettings:Domains:SPA").Value;
                        builder.WithOrigins(qtdDomain)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });

                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin();
                });

                options.AddPolicy("AllowQTD", policy =>
                {
                    var qtdDomain = Configuration.GetSection("ApplicationSettings:Domains:Qtd").Value;
                    policy.WithOrigins(qtdDomain);
                });
            });

       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IAntiforgery antiforgery)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c => c.RouteTemplate = "QTD2/API/Auth/docs/{documentName}/auth.json");
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "QTD2/API/Auth/docs";
                c.SwaggerEndpoint("/QTD2/API/Auth/docs/v1/auth.json", "QTD2.API.Authentication v1");
            });

            // global cors policy
            //app.UseCors();

            app.UseCustomErrorHandling();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x.AllowAnyMethod()
                  .AllowAnyHeader()
                  .SetIsOriginAllowed(origin => true) // allow any origin
                  .AllowCredentials());
            app.UseCors("DisAllowAllExceptQtdSpa");
            app.UseUrlRewrite();
            app.UseBodyDecoding();
            app.UseBodyEncoding();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
