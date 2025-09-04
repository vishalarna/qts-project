using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QTD2.Application.Startup;
using QTD2.Application.Startup.Middleware.Extensions;
using System;

namespace QTD2.Web.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddHttpContextAccessor();

            services.AddHttpClients(Configuration);

            services.AddDataServices(Configuration);

            services.AddDomainServices(Configuration);

            services.AddInfrastructureServices(Configuration);

            services.AddApplicationServices(Configuration);

            services.ConfigureAuthentication_Client(Configuration);

            services.AddAuthorizationPolicies(Configuration);

            services.AddLocalization();

            services.AddTransient<Infrastructure.Identity.Interfaces.IIdentityBuilder, Identity.AdminIdentityBuilder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityMiddleware(serviceProvider.GetService<Infrastructure.Identity.Interfaces.IIdentityBuilder>());
            app.UseAuthorization();

            app.UseLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
