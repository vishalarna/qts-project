using System;
using System.Collections.Generic;
using System.Net.Http;
using QTD2.Infrastructure.Scorm.Settings;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using QTD2.Infrastructure.Rustici.EngineApi;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;

namespace QTD2.Infrastructure.HttpClients
{
    public class ScormEngineService
    {
        private HttpClient Client { get; }
        private ScormServerSettings Settings;

        private string _api_username = "apiuser";
        private string _api_password = "password";

        private string _encodedAuthenticationString;

        private ILogger<ScormEngineService> _logger;

        public ScormEngineService(HttpClient client, ILogger<ScormEngineService> logger, IOptions<ScormServerSettings> options)
        {
            Client = client;
            Settings = options.Value;
            _logger = logger;

            if (Settings.UseMock)
            {
                var mockHandler = new MockScormHandler(Settings.BaseUrl);
                Client = new HttpClient(mockHandler);
            }
            else
            {
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.BaseAddress = new Uri(options.Value.BaseUrl);
            }

            string authenticationString = $"{_api_username}:{_api_password}";
            _encodedAuthenticationString = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));

            Client.DefaultRequestHeaders.Add("Authorization", "Basic " + _encodedAuthenticationString);
        }

        public async Task<PingResponse> Ping()
        {
            Client.DefaultRequestHeaders.Add("EngineTenantName", "qtdmigrationtest");

            string url = Settings.BaseUrl + "/ping";
            var response = await Client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            PingResponse pingResponse = JsonConvert.DeserializeObject<PingResponse>(json);

            return pingResponse;
        }

        public async Task<CreateUploadImportCourseJobResponse> CreateUploadAndImportCourseJobAsync(CreateUploadImportCourseSchema courseJob)
        {
            Client.DefaultRequestHeaders.Add("EngineTenantName", courseJob.EngineTenantName);

            string url = Settings.BaseUrl + "/courses/upload?courseId=" + courseJob.CourseId;

            _logger.LogInformation(" Calling CreateUpAndImportCourse: " +  url);

            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(courseJob.File.Contents), "file", courseJob.File.FileName);
            var response = await Client.PostAsync(url, content);

            var html = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("Response from CreateUpAndImportCourse: " + html);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            CreateUploadImportCourseJobResponse courseJobResponse = JsonConvert.DeserializeObject<CreateUploadImportCourseJobResponse>(json);

            return courseJobResponse;
        }

        public async Task<CreateRegistrationWithLaunchLinkResponse> CreateRegistrationWithLaunchLinkAsync(CreateRegistrationWithLaunchLinkSchema registrationSchema, string engineTenantName)
        {
            Client.DefaultRequestHeaders.Add("EngineTenantName", engineTenantName);

            string url = Settings.BaseUrl + "/registrations/withLaunchLink";

            Newtonsoft.Json.Serialization.DefaultContractResolver contractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
            {
                NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
            };

            var content = JsonConvert.SerializeObject(registrationSchema, new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            });
            var response = await Client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

            var json = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            CreateRegistrationWithLaunchLinkResponse registrationLinkResponse = JsonConvert.DeserializeObject<CreateRegistrationWithLaunchLinkResponse>(json);
            return registrationLinkResponse;
        }

        public async Task<string> CreateOrUpdateTenantAsync(string tenantName, CreateOrUpdateTenantSchema updateTenantSchema)
        {
            Client.DefaultRequestHeaders.Add("EngineTenantName", updateTenantSchema.EngineTenantName);

            string url = Settings.BaseUrl + "/appManagement/tenants/" + tenantName;

            var content = JsonConvert.SerializeObject(updateTenantSchema);
            var response = await Client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return json.ToString();
        }

        //giving return type as string because this returns only string like "Success" as per scorm document.
        public async Task<string> SetApplicationConfigurationAsync(SetApplicationConfigurationSchema appConfiguration)
        {
            Client.DefaultRequestHeaders.Add("EngineTenantName", appConfiguration.EngineTenantName);

            string url = Settings.BaseUrl + "/appManagement/configuration";

            var content = JsonConvert.SerializeObject(appConfiguration);
            var response = await Client.PostAsync(url, new StringContent(content));

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            //To Be Discussed As per the scorm document string is returned
            return json.ToString();

        }

        public async Task<ApplicationConfigurationResponse> GetApplicationConfigurationAsync(bool singleSco, bool includeMetadata = false, bool includeHiddenettings = false,
            bool includeSecretSettings = false, bool processReplacementTokens = true)
        {
            //Client.DefaultRequestHeaders.Add("EngineTenantName", courseJob.EngineTenantName);

            string url = Settings.BaseUrl + "/appManagement/configuration";

            var response = await Client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            ApplicationConfigurationResponse appConfigResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ApplicationConfigurationResponse>(json);
            return appConfigResponse;
        }

        public async Task<string> CreateSubscriptionAsync(SubscriptionDefinitionSchema subscriptionDefinition)
        {
            string url = Settings.BaseUrl + "/appManagement/subscriptions";

            Newtonsoft.Json.Serialization.DefaultContractResolver contractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
            {
                NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
            };

            var content = JsonConvert.SerializeObject(subscriptionDefinition, new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            });

            var response = await Client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

            var json = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
  
            return json.ToString();
        }
    }
}
