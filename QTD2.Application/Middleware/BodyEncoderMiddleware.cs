using System;
using System.IO;
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
    public class BodyEncoderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHasher _hasher;
        private readonly Settings.DomainSettings _domainSettings;
        private readonly ILogger<BodyEncoderMiddleware> _logger;

        public BodyEncoderMiddleware(
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
            var originalBody = context.Response.Body;

            using (var memStream = new MemoryStream())
            {
                context.Response.Body = memStream;
                await _next(context);

                var referrer = context.Request.Headers["Referer"];

                if (!String.IsNullOrEmpty(referrer) && shouldHash(context.Response.ContentType, new Uri(referrer)))
                {
                    memStream.Position = 0;
                    string responseBody = await new StreamReader(memStream).ReadToEndAsync();

                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        var jobject = JObject.Parse(responseBody);
                        var candidiates = jobject.DescendantsAndSelf().OfType<JProperty>().Where(r => r.Name.ToUpper().EndsWith("ID"));
                        var candidatesList = jobject.DescendantsAndSelf().OfType<JProperty>().Where(r => r.Name.ToLower().EndsWith("ids"));

                        foreach (var candidate in candidiates)
                        {
                            candidate.Value = _hasher.Encode(candidate.Value.ToString());
                        }

                        foreach (var candidate in candidatesList)
                        {
                            // loop is required because, there would be more than 1 array in a ClassOption Parameter
                            // e.g in TaskOptions its enablingObjectiveIds,PositionIds and ProcedureIds
                            foreach (var token in candidate.Value.Children().OfType<JValue>())
                            {
                                token.Value = _hasher.Encode(token.Value.ToString());
                            }
                        }

                        string json = jobject.ToString(Newtonsoft.Json.Formatting.None);

                        context.Response.Headers.Remove("Content-Length");

                        context.Response.Body = originalBody;
                        await context.Response.WriteAsync(json);
                    }
                }
                else
                {
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    await context.Response.Body.CopyToAsync(originalBody);
                    context.Response.Body = originalBody;
                }
            }
        }

        //TODO
        //Refactor
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
