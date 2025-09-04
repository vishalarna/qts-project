using System.IO;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using QTD2.Infrastructure.Hashing.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace QTD2.Application.Middleware
{
    public class BodyDecoderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHasher _hasher;
        private readonly Settings.DomainSettings _domainSettings;

        private readonly ILogger<BodyEncoderMiddleware> _logger;

        public BodyDecoderMiddleware(
            RequestDelegate next,
            IHasher hasher,
            IOptions<Settings.DomainSettings> domainSettingOptions,
            ILogger<BodyEncoderMiddleware> logger)
        {
            _hasher = hasher;
            _next = next;
            _domainSettings = domainSettingOptions.Value;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var referrer = context.Request.Headers["Referer"];

            if (!String.IsNullOrEmpty(referrer) && shouldHash(context.Request.ContentType, new Uri(referrer)))
            {
                var content = await getContent(request);

                if (!string.IsNullOrEmpty(content))
                {
                    var jobject = JObject.Parse(content);
                    var candidiates = jobject.DescendantsAndSelf().OfType<JProperty>().Where(r => r.Name.ToLower().EndsWith("id"));
                    var candidatesList = jobject.DescendantsAndSelf().OfType<JProperty>().Where(r => r.Name.ToLower().EndsWith("ids"));

                    foreach (var candidate in candidiates)
                    {
                        candidate.Value = _hasher.Decode(candidate.Value.ToString());
                    }

                    foreach (var candidate in candidatesList)
                    {
                        // loop is required because, there would be more than 1 array in a ClassOption Parameter
                        // e.g in TaskOptions its enablingObjectiveIds,PositionIds and ProcedureIds
                        foreach (var token in candidate.Value.Children().OfType<JValue>())
                        {
                            token.Value = _hasher.Decode(token.Value.ToString());
                        }
                    }

                    var requestContent = new StringContent(jobject.ToString(), Encoding.UTF8, "application/json");
                    request.Body = await requestContent.ReadAsStreamAsync();
                }
            }

            await _next(context);
        }

        private async Task<string> getContent(HttpRequest request)
        {
            var bodyStr = string.Empty;

            using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = await reader.ReadToEndAsync();
            }

            return bodyStr;
        }

        private bool shouldHash(string contentType, Uri referrer)
        {
            _logger.LogInformation($"Asking should hash {contentType} and {referrer.ToString()}");

            if (contentType == null) return false;

            if (!contentType.ToUpper().Contains("APPLICATION/JSON"))
                return false;

            return referrer.ToString().ToUpper().Contains(new Uri(_domainSettings.SPA).ToString().ToUpper());
        }
    }
}