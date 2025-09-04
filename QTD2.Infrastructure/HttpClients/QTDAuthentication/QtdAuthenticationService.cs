using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.QTD2Auth.Settings;
using QTD2.Infrastructure.Identity.Settings;
using QTD2.Infrastructure.Identity.Interfaces;
using QTD2.Infrastructure.JWT;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using QTD2.Infrastructure.Model.AdminMessageAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QTD2.Infrastructure.HttpClients
{
    public class QtdAuthenticationService
    {
        public static HttpClient Client { get; set; }
        private static QTDAuthSettings AuthSettings;
        private static IHttpContextAccessor _httpContextAccessor;
        private readonly IJWTBuilder _iJWTBuilder;
        private readonly IClaimsBuilderFactory _claimsBuilderFactory;
        private ILogger<QtdAuthenticationService> _logger;
        private string _jwt;

        public QtdAuthenticationService(
            IHttpContextAccessor httpContextAccessor,
            HttpClient client,
            IClaimsBuilderFactory claimsBuilderFactory,
            IJWTBuilder iJWTBuilder,
            ILogger<QtdAuthenticationService> logger,
            IOptions<QTDAuthSettings> options)
        {
            Client = client;
            _httpContextAccessor = httpContextAccessor;
            AuthSettings = options.Value;
            _logger = logger;
            _claimsBuilderFactory = claimsBuilderFactory;
            _iJWTBuilder = iJWTBuilder;

            _jwt = GetSystemJwt();

            if (AuthSettings.UseMock)
            {
                var mockHandler = new MockQtdAuthenticationServiceHandler(AuthSettings.BaseUrl);
                Client = new HttpClient(mockHandler);
            }
        }

        public UsersService Users
        {
            get { return new UsersService(_jwt); }
        }

        public InstanceService Instances
        {
            get { return new InstanceService(_logger, _jwt); }
        }

        public ClientsService Clients
        {
            get { return new ClientsService(_logger, _jwt); }
        }

        public AdminMessageAuthenticationService AuthMessages
        {
            get { return new AdminMessageAuthenticationService(_logger, _jwt); }
        }

        public class ClientsService
        {
            ILogger<QtdAuthenticationService> _logger;

            private string _jwt;

            public ClientsService(ILogger<QtdAuthenticationService> logger, string jwt)
            {
                _logger = logger;
                _jwt = jwt;
            }

            public Client Create(Client client)
            {
                throw new NotImplementedException();
            }

            public Client Update(Client client)
            {
                throw new NotImplementedException();
            }

            public Client Get(string clientName)
            {
                throw new NotImplementedException();
            }

            public async Task<Client> GetByInstanceNameAsync(string instanceName)
            {
                string url = AuthSettings.BaseUrl + "/instances/" + instanceName + "/client";

                var msg = new HttpRequestMessage(HttpMethod.Get, url);
                msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);

                var response = await Client.SendAsync(msg);

                // Return the response content as a string
                var json = await response.Content.ReadAsStringAsync();
                Client client = Newtonsoft.Json.JsonConvert.DeserializeObject<Client>(json);

                return client;
            }

            public List<Client> Get()
            {
                throw new NotImplementedException();
            }

            public void Delete(string clientName)
            {
                throw new NotImplementedException();
            }
        }

        public class UsersService
        {
            private string _jwt;

            public UsersService(string jwt)
            {
                _jwt = jwt;
            }

            public AppUser Create(AppUser user)
            {
                throw new NotImplementedException();
            }

            public AppUser Update(AppUser user)
            {
                throw new NotImplementedException();
            }

            public AppUser Get(string username)
            {
                throw new NotImplementedException();
            }

            public List<AppUser> Get()
            {
                throw new NotImplementedException();
            }

            public void Delete(string username)
            {
                throw new NotImplementedException();
            }
        }

        public class InstanceService
        {
            ILogger<QtdAuthenticationService> _logger;
            private string _jwt;

            public InstanceService(ILogger<QtdAuthenticationService> logger, string jwt)
            {
                _logger = logger;
                _jwt = jwt;
            }

            public async Task<InstanceSetting> GetInstanceSettingsAsync(string instanceName)
            {
                string url = AuthSettings.BaseUrl + "/instances/" + instanceName + "/settings";

                var msg = new HttpRequestMessage(HttpMethod.Get, url);
                msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);

                // Make a request to a protected resource using the HttpClient
                var response = await Client.SendAsync(msg);

                // Return the response content as a string
                var json = await response.Content.ReadAsStringAsync();
                InstanceSetting setting = Newtonsoft.Json.JsonConvert.DeserializeObject<InstanceSetting>(json);

                return setting;
            }

            public async Task<List<InstanceSetting>> GetAsync()
            {
                string url = AuthSettings.BaseUrl + "/instances/settings";

                var msg = new HttpRequestMessage(HttpMethod.Get, url);
                msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);

                var response = await Client.SendAsync(msg);

                var json = await response.Content.ReadAsStringAsync();
                dynamic jobject = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                _logger.LogInformation(json);
                var locList = jobject.locList.ToString(Newtonsoft.Json.Formatting.None);
                List<InstanceSetting> settings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InstanceSetting>>(locList);
                return settings;
            }

            public async Task<List<InstanceSetting>> GetActiveAsync()
            {
                string url = AuthSettings.BaseUrl + "/instances/settings/active";

                var msg = new HttpRequestMessage(HttpMethod.Get, url);
                msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);

                var response = await Client.SendAsync(msg);

                var json = await response.Content.ReadAsStringAsync();
                dynamic jobject = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                _logger.LogInformation(json);
                var locList = jobject.locList.ToString(Newtonsoft.Json.Formatting.None);
                List<InstanceSetting> settings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InstanceSetting>>(locList);
                return settings;
            }

            public async Task<InstanceSetting> GetInstanceSettingsByScormTenant(string tenantName)
            {
                string url = AuthSettings.BaseUrl + $"/instances/scorm/{tenantName}/settings";

                var msg = new HttpRequestMessage(HttpMethod.Get, url);
                msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);

                var response = await Client.SendAsync(msg);
                var json = await response.Content.ReadAsStringAsync();
                InstanceSetting settings = Newtonsoft.Json.JsonConvert.DeserializeObject<InstanceSetting>(json);

                return settings;
            }
        }

        public class AdminMessageAuthenticationService
        {
            ILogger<QtdAuthenticationService> _logger;
            private string _jwt;

            public AdminMessageAuthenticationService(ILogger<QtdAuthenticationService> logger, string jwt)
            {
                _logger = logger;
                _jwt = jwt;
            }

            public async Task<List<AdminMessageAuth>> GetAdminMessageAsync(string instance)
            {
                string url = AuthSettings.BaseUrl + $"/adminMessage/{instance}";

                var msg = new HttpRequestMessage(HttpMethod.Get, url);
                msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);

                var response = await Client.SendAsync(msg);

                var json = await response.Content.ReadAsStringAsync();
                dynamic jobject = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                _logger.LogInformation(json);
                var result = jobject.result.ToString(Newtonsoft.Json.Formatting.None);
                List<AdminMessageAuth> messages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AdminMessageAuth>>(result);
                return messages;
            }

            public async Task<List<AdminMessageAuth>> UpdateAdminMessageAuthReceivedStatusAsync(string instance, AdminMessageSourceIdOptions options)
            {
                string url = AuthSettings.BaseUrl + $"/adminMessage/{instance}";

                var msg = new HttpRequestMessage(HttpMethod.Put, url);
                msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);

                string json = JsonConvert.SerializeObject(options);
                msg.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await Client.SendAsync(msg);
                response.EnsureSuccessStatusCode(); 

                var responseBody = await response.Content.ReadAsStringAsync();

                var jobject = JsonConvert.DeserializeObject<JObject>(responseBody);
                var resultJson = jobject["result"]?.ToString(); 

                var messages = JsonConvert.DeserializeObject<List<AdminMessageAuth>>(resultJson);

                return messages;
            }

        }

        public string GetSystemJwt()
        {
            ClaimsBuilderOptions options = new ClaimsBuilderOptions(true);

            var builder = _claimsBuilderFactory.GetBuilder(options);

            var claims = builder.Build(null, null, options);

            return _iJWTBuilder.CreateJWTTokenString(claims, new JWTOptions()
            {
                ExpirationMinutes = 2,
            });
        }

    }
}
