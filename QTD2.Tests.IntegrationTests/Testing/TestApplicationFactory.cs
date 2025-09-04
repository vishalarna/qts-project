using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace QTD2.Tests.IntegrationTests.Testing
{
    public class TestApplicationFactory<TTestStartup> : WebApplicationFactory<TTestStartup> where TTestStartup : class
    {
        public TestApplicationFactory() : base() { }

        protected override IHostBuilder CreateHostBuilder()
        {
            var host = Host.CreateDefaultBuilder()
                            .ConfigureWebHost(builder =>
                            {
                                builder.UseStartup<TTestStartup>();
                                builder.UseEnvironment("Sqlite");
                                builder.UseSetting("integration_testing", "true");

                                System.Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
                            })
                            .ConfigureAppConfiguration((context, conf) =>
                            {
                                var projectDir = Directory.GetCurrentDirectory();
                                var configPath = Path.Combine(projectDir, "appsettings.json");

                                conf.AddJsonFile(configPath);
                                conf.AddEnvironmentVariables();
                            });

            return host;
        }
    }
}
