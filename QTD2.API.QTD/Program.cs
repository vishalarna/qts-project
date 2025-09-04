using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace QTD2.API.QTD
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var startupTasks = scope.ServiceProvider.GetServices<QTD2.Infrastructure.Jobs.Interfaces.IJob>();

                foreach (var startupTask in startupTasks)
                {
                    if (startupTask.RunAtStartup)
                    {
                        try
                        {
                            await startupTask.ExecuteAsync();
                        }
                        catch(System.Exception e)
                        {
                            
                        }                       
                    }                      
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                });

    }
}
