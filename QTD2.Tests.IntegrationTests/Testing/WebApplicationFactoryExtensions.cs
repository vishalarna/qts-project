using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Identity.Settings;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QTD2.Tests.IntegrationTests.Testing
{
    public static class ControllerExtensions
    {
        public static async Task<HttpClient> CreateClientWithTestAuthAsync<T>(this TestApplicationFactory<T> factory, ClaimsBuilderOptions options, string username) where T : class
        {
            using (var scope = factory.Services.CreateScope())
            {
                IUserService userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                var client = factory.CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

                var user = new AppUser
                {
                    UserName = "michael@qts.com",
                    NormalizedEmail = "MICHAEL@QTS.COM",
                    NormalizedUserName = "MICHAEL@QTS.COM",
                    TwoFactorEnabled = false,
                    EmailConfirmed = true,
                    Email = "michael@qts.com"
                };
                var jwt = await userService.GetJwtAsync(options, user);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                return client;
            }
        }
    }
}
