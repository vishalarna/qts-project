
using Microsoft.Extensions.DependencyInjection;
using QTD2.API.Authentication;
using QTD2.Infrastructure.Jobs.Interfaces;
using System;

namespace QTD2.Tests.IntegrationTests.Testing.Fixures
{
    public class AuthenticationFixture : IFixture<Startup>, IDisposable
    {
        public TestApplicationFactory<Startup> Factory { get; private set; }
        public Application.Interfaces.Services.Authentication.INotificationService NotificationService {get; set;}
        public Application.Interfaces.Services.Shared.IUserService UserService { get; set; }

        public AuthenticationFixture()
        {
           
        }

        public async System.Threading.Tasks.Task StartupAsync()
        {
            Factory = new TestApplicationFactory<Startup>();
            var scope = Factory.Services.CreateScope();

            //using (var scope = Factory.Services.CreateScope())
            //{
                var startupTasks = scope.ServiceProvider.GetServices<IJob>();
                NotificationService = scope.ServiceProvider.GetService<Application.Interfaces.Services.Authentication.INotificationService>();
                UserService = scope.ServiceProvider.GetService<Application.Interfaces.Services.Shared.IUserService>();

                foreach (var startupTask in startupTasks)
                {
                    await startupTask.ExecuteAsync();
                }
          //  }
        }

        public void Dispose()
        {
            //delete database
        }
    }
}
