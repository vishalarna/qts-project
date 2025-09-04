using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using System.IO;
using Newtonsoft.Json;
using Quartz;
using QTD2.Application.Attributes;
using QTD2.Application.Startup;
using QTD2.Application.Startup.Middleware.Extensions;
using QTD2.Application.Jobs.Startup;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Application.Jobs.ClassScheduleJobs;
using QTD2.Application.Jobs.Notifications;
using System;
using QTD2.Application.Jobs.EmployeeJobs;
using System.Collections.Specialized;
using QTD2.Infrastructure.Notification;
using QTD2.Infrastructure.Notification.Settings;
using QTD2.Infrastructure.Notification.Interfaces;

namespace QTD2.API.QTD
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
            services.AddControllers(config =>
            {
                config.Filters.Add(new AuthorizeFilter(Application.Authorization.Policies.Authenticated));
                config.Filters.Add(new QTD2APIControllerAtribute());
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QTD2.API.QTD", Version = "v1" });
            });

            string baseUrl = Configuration.GetSection("Scorm:BaseUrl").Value;
            services.AddCors(options =>
            {
                options.AddPolicy("ScormAPIPolicy",
                    builder =>
                    {
                        builder.WithOrigins("*")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            services.AddHttpContextAccessor();

            services.AddSharedHttpClients(Configuration);
            services.AddQTDHttpClients(Configuration);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());

            services.AddDatabaseServices(Configuration);
            services.AddQTDDatabaseServices(Configuration);

            // these are set to be removed in the future
            services.AddAuthenticationDatabase(Configuration);
            services.AddAuthenticationDataServices(Configuration);
            services.AddAuthenticationDomainServices(Configuration);
            services.AddAuthenticationApplicationServices(Configuration);

            // end

            services.AddQuartz(new NameValueCollection
            {
                { "quartz.threadPool.type", "Quartz.Simpl.DefaultThreadPool, Quartz" },
                { "quartz.threadPool.maxConcurrency", "1" }
            },q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var classScheduleJobKey = new JobKey("classScheduleJob");
                var certificationJobKey = new JobKey("certificationExpirationJob");
                var notificationJobKey = new JobKey("notificationJob");
                var empCompletionJobKey = new JobKey("adminEmpPortalCompletionsNotificationJob");
                int empCompletionIntervalInMin = Convert.ToInt32(Configuration.GetSection("EmailNotifications:RunPortalCompletions").Value);
                var inactivateExpiredEmployeeCertificationsJobKey = new JobKey("inactivateExpiredEmployeeCertificationsJob", "EmployeeJobsGroup");
                var inactivateExpiredEmployeePositionsJobKey = new JobKey("inactivateExpiredEmployeePositionsJob", "EmployeeJobsGroup");
                var publicClassScheduleRequestJobKey = new JobKey("publicClassScheduleRequestJob");

                q.AddJob<ClassScheduleNotificationJob>(opts => opts.WithIdentity(classScheduleJobKey));
                q.AddTrigger(opts => opts.ForJob(classScheduleJobKey)
                    .WithIdentity("classScheduleJob-trigger")
                    .WithCronSchedule("13 0/2 * 1/1 * ? *"));

                var EMPSettingsJobKey = new JobKey("EMPSEttingsJob");

                q.AddJob<EMPSettingsJob>(opts => opts.WithIdentity(EMPSettingsJobKey));
                q.AddTrigger(opts => opts.ForJob(EMPSettingsJobKey)
                    .WithIdentity("EMPSettingsJobRepeat-trigger")
                    .WithCronSchedule("20 0/2 * 1/1 * ? *"));

                q.AddJob<QTD2.Application.Jobs.Notifications.NotificationJob>(opts => opts.WithIdentity(notificationJobKey));
                q.AddTrigger(opts => opts.ForJob(notificationJobKey)
                    .WithIdentity("notificationJob-trigger")
                    .WithCronSchedule("40 0/2 * 1/1 * ? *"));

                //commented this as this is not yet implemented

                q.AddJob<CertificationExpirationNotificationJob>(opts => opts.WithIdentity(certificationJobKey));
                q.AddTrigger(opts => opts.ForJob(certificationJobKey)
                    .WithIdentity("certificationExpirationJob-trigger")
                    .WithCronSchedule("3 1-59/2 * 1/1 * ? *"));

                q.AddJob<AdminEmpPortalCompletionsNotificationJob>(opts => opts.WithIdentity(empCompletionJobKey));
                q.AddTrigger(opts => opts.ForJob(empCompletionJobKey)
                    .WithIdentity("empCompletionJob-trigger")
                     .WithCronSchedule("50 1-59/2 * 1/1 * ? *"));

                q.AddJob<PublicClassScheduleRequestNotificationJob>(opts => opts.WithIdentity(publicClassScheduleRequestJobKey));
                q.AddTrigger(opts => opts.ForJob(publicClassScheduleRequestJobKey)
                    .WithIdentity("publicClassScheduleRequestJob-trigger")
                    .WithCronSchedule("13 0/2 * 1/1 * ? *"));
                q.AddJob<InactivateExpiredEmployeePositionsJob>(opts => opts.WithIdentity(inactivateExpiredEmployeePositionsJobKey));
                q.AddTrigger(opts => opts.ForJob(inactivateExpiredEmployeePositionsJobKey)
                    .WithIdentity("inactivateExpiredEmployeePositionsJob-trigger")
                    .WithPriority(2)
                    .WithCronSchedule("11 0 */2 * * ?"));

                q.AddJob<InactivateExpiredEmployeeCertificationsJob>(opts => opts.WithIdentity(inactivateExpiredEmployeeCertificationsJobKey));
                q.AddTrigger(opts => opts.ForJob(inactivateExpiredEmployeeCertificationsJobKey)
                    .WithIdentity("inactivateExpiredEmployeeCertificationsJob-trigger")
                    .WithPriority(2)
                    .WithCronSchedule("19 0 */2 * * ?"));
            });

            services.AddQuartzHostedService(q =>
            { 
                q.WaitForJobsToComplete = true;
            });


            services.AddQTDDataServices(Configuration);

            services.AddQTDDomainServices(Configuration);

            services.AddInfrastructureServices(Configuration);

            services.AddQTDInstanceFetcher(Configuration);

            services.AddQTDInfrastructureServices(Configuration);

            services.AddQTDApplicationServices(Configuration);

            services.ConfigureIdentity(Configuration);

            services.ConfigureJWTAuthentication_Client(Configuration);

            services.AddLocalization(Configuration);

            services.RegisterSettings(Configuration);

            services.AddAuthorizationPolicies(Configuration);

            services.AddTransient<QTD2.Infrastructure.Jobs.Interfaces.IJob, QTD2DatabaseSetup>();
            services.AddTransient<QTD2.Infrastructure.Jobs.Interfaces.IJob, SubscribeToScormEvents>();

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
                options.MaxRequestBodySize = null;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ITestSchedulerService _testSchedulerService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QTD2.API.QTD v1"));

            app.UseCustomErrorHandling();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseUrlRewrite();
            app.UseBodyDecoding();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBodyEncoding();

            // global cors policy
            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());


            //app.UseCors("ScormAPIPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                !Directory.Exists(Directory.GetCurrentDirectory() + "\\img") ? Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\img").FullName:Path.Combine(Directory.GetCurrentDirectory(), "img")) ,
                RequestPath = "/img" // Define the URL path to access the images
            });
        }
    }
}
