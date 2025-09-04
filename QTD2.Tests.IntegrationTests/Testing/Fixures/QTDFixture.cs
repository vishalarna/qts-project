using Microsoft.Extensions.DependencyInjection;
using QTD2.API.QTD;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Jobs.Interfaces;
using System;

namespace QTD2.Tests.IntegrationTests.Testing.Fixures
{
    public class QTDFixture : IFixture<Startup>, IDisposable
    {
        public TestApplicationFactory<Startup> Factory { get; private set; }
        public IUserService UserService { get; private set; }
        public Application.Interfaces.Services.QTD.INotificationService NotificationService { get; private set; }
        public Application.Interfaces.Services.QTD.IJobNotificationService JobNotificationService { get; private set; }

        public QTDFixture()
        {

        }

        public async System.Threading.Tasks.Task StartupAsync()
        {
            Factory = new TestApplicationFactory<Startup>();
            var scope = Factory.Services.CreateScope();

            //using (var scope = Factory.Services.CreateScope())
            //{
            UserService = scope.ServiceProvider.GetRequiredService<IUserService>();
            NotificationService = scope.ServiceProvider.GetRequiredService<Application.Interfaces.Services.QTD.INotificationService>();
            JobNotificationService = scope.ServiceProvider.GetRequiredService<Application.Interfaces.Services.QTD.IJobNotificationService>();

            var startupTasks = scope.ServiceProvider.GetServices<IJob>();

            foreach (var startupTask in startupTasks)
            {
                await startupTask.ExecuteAsync();
            }
            // }
        }

        public void Dispose()
        {
            //delete database
        }
    }
}
