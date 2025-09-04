using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QTD2.Application.Startup
{
    public static class HttpClients
    {
        public static void AddSharedHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<Infrastructure.HttpClients.ScormEngineService>();
            services.AddHttpClient<Infrastructure.HttpClients.QtdAuthenticationService>();
        }

        public static void AddQTDHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            
        }
    }
}
