using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using QTD2.Infrastructure.Hashing.Interfaces;
using Microsoft.Extensions.Options;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Middleware
{
    public class UrlRewriteMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHasher _hasher;
        private readonly Settings.DomainSettings _domainSettings;

        public UrlRewriteMiddleware(
                RequestDelegate next,
                IHasher hasher,
                IOptions<Settings.DomainSettings> domainSettingOptions)
        {
            _hasher = hasher;
            _next = next;
            _domainSettings = domainSettingOptions.Value; 
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var referrer = context.Request.Headers["Referer"];
            if (!String.IsNullOrEmpty(referrer) && shouldHash(new Uri(referrer)))
            {
                var routeValues = context.Request.RouteValues.Where(r => r.Key.ToUpper().EndsWith("ID"));
                foreach (var routeValue in routeValues)
                {
                    string v = _hasher.Decode(routeValue.Value.ToString());

                    if (!int.TryParse(v, out int id))
                    {
                        // log
                        // how does this play with the exception throw in the hasher
                        throw new QTDServerException("Value failed to decode");
                    }

                    context.Request.RouteValues[routeValue.Key] = v;
                }
            }

            await _next(context);
        }

        //TODO Refactor
        private bool shouldHash(Uri referrer)
        {
            var spaUri = new Uri(_domainSettings.SPA.ToUpper());
            return referrer.ToString().ToUpper().Contains(spaUri.ToString().ToUpper());
        }
    }
}
