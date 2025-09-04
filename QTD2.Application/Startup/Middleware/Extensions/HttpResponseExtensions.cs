using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;

namespace QTD2.Application.Startup.Middleware.Extensions
{
    public static class HttpResponseExtensions
    {
        public static Task<bool> AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            return Task.FromResult(true);
        }
        public static Task<bool> AddApplicationError(this HttpResponse response, string message, int statusCode)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.StatusCode = statusCode;
            return Task.FromResult(true);
        }
    }
}
