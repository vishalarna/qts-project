using System;
using System.Collections.Generic;
using System.Linq;
using QTD2.Infrastructure.QTD2Auth.Settings;
using QTD2.Infrastructure.Identity.Settings;
using System.Net.Http;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace QTD2.Infrastructure.HttpClients
{
    public class QTDHttpClient
    {
        private readonly HttpClient _httpClient;
        private static QTDAuthSettings AuthSettings;
        public QTDHttpClient(HttpClient httpClient, IOptions<QTDAuthSettings> options)
        {
            _httpClient = httpClient;
            AuthSettings = options.Value;
        }

        public async Task<List<Claim>> GetIdentityAsync(string username, string databseName)
        {
            string url = AuthSettings.BaseUrl + "/instances/" + databseName;

            var msg = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(msg);

            // Check the response status
            response.EnsureSuccessStatusCode();

            // Obtain the list of claims from the response
            var json = await response.Content.ReadAsStringAsync();
            List<Claim> claims = JsonSerializer.Deserialize<List<Claim>>(json);
            return claims;
        }
    }
}
