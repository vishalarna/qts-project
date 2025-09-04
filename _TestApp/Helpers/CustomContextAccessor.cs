using Microsoft.AspNetCore.Http;

namespace _TestApp.Helpers
{
    public class CustomContextAccessor : IHttpContextAccessor
    {
        public HttpContext HttpContext { get; set; }
    }
}
