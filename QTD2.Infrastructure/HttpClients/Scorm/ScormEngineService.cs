using System;
using System.Collections.Generic;
using System.Net.Http;
using QTD2.Infrastructure.Scorm.Settings;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using QTD2.Infrastructure.Rustici.EngineApi;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;
using System.Linq.Expressions;
using System.Linq;
using System.Text.Json.Serialization;

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
                var mockHandler = new MockScormHandler(Settings.FullApiPath);
                Client = new HttpClient(mockHandler);
            }
            else
            {
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.BaseAddress = new Uri(options.Value.FullApiPath);
            }

            string authenticationString = $"{_api_username}:{_api_password}";
            _encodedAuthenticationString = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));

            Client.DefaultRequestHeaders.Add("Authorization", "Basic " + _encodedAuthenticationString);
        }

        public async Task<PingResponse> Ping()
        {
            Client.DefaultRequestHeaders.Add("EngineTenantName", "qtdmigrationtest");

            string url = Settings.FullApiPath + "/ping";
            var response = await Client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            PingResponse pingResponse = JsonSerializer.Deserialize<PingResponse>(json);

            return pingResponse;
        }

        public async Task<CreateUploadImportCourseJobResponse> CreateUploadAndImportCourseJobAsync(CreateUploadImportCourseSchema courseJob)
        {
            Client.DefaultRequestHeaders.Add("EngineTenantName", courseJob.EngineTenantName);

            string url = Settings.FullApiPath + "/courses/upload?courseId=" + courseJob.CourseId + "&mayCreateNewVersion=" + courseJob.MayCreateNewVersion.ToString();

            _logger.LogInformation(" Calling CreateUpAndImportCourse: " + url);

            var content = new MultipartFormDataContent();

            HttpResponseMessage response;

            using (var fileStream = courseJob.File.OpenReadStream())
            {
                content.Add(new StreamContent(fileStream), "file", courseJob.File.FileName);
                fileStream.Seek(0, SeekOrigin.Begin);
                response = await Client.PostAsync(url, content);
            }

            var html = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("Response from CreateUpAndImportCourse: " + html);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            CreateUploadImportCourseJobResponse courseJobResponse = JsonSerializer.Deserialize<CreateUploadImportCourseJobResponse>(json);

            return courseJobResponse;
        }

        public async Task<CreateRegistrationWithLaunchLinkResponse> CreateRegistrationWithLaunchLinkAsync(CreateRegistrationWithLaunchLinkSchema registrationSchema, string engineTenantName)
        {
            Client.DefaultRequestHeaders.Add("EngineTenantName", engineTenantName);

            string url = Settings.FullApiPath + "/registrations/withLaunchLink";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  
            };

            var content = JsonSerializer.Serialize(registrationSchema, options);

            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(url, httpContent);

            var json = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var registrationLinkResponse = JsonSerializer.Deserialize<CreateRegistrationWithLaunchLinkResponse>(json, options);

            return registrationLinkResponse;
        }

        public async Task<string> CreateOrUpdateTenantAsync(string tenantName, CreateOrUpdateTenantSchema updateTenantSchema)
        {
            Client.DefaultRequestHeaders.Add("EngineTenantName", updateTenantSchema.EngineTenantName);

            string url = Settings.FullApiPath + "/appManagement/tenants/" + tenantName;

            var content = JsonSerializer.Serialize(updateTenantSchema);
            var response = await Client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return json.ToString();
        }

        //giving return type as string because this returns only string like "Success" as per scorm document.
        public async Task<string> SetApplicationConfigurationAsync(SetApplicationConfigurationSchema appConfiguration)
        {
            Client.DefaultRequestHeaders.Add("EngineTenantName", appConfiguration.EngineTenantName);

            string url = Settings.FullApiPath + "/appManagement/configuration";

            var content = JsonSerializer.Serialize(appConfiguration);
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

            string url = Settings.FullApiPath + "/appManagement/configuration";

            var response = await Client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            ApplicationConfigurationResponse appConfigResponse = JsonSerializer.Deserialize<ApplicationConfigurationResponse>(json);
            return appConfigResponse;
        }

        public async Task<SubscriptionResponseSchema> GetSubscriptionsAsync(string more, string topic, string subtopic)
        {
            //Client.DefaultRequestHeaders.Add("EngineTenantName", courseJob.EngineTenantName);

            string url = Settings.FullApiPath + "/appManagement/subscriptions?more=" + more + "&topic=" + topic + "&subTopic=" + subtopic;

            var response = await Client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            SubscriptionResponseSchema appConfigResponse = JsonSerializer.Deserialize<SubscriptionResponseSchema>(json);
            return appConfigResponse;
        }

        public async Task<string> CreateSubscriptionAsync(SubscriptionDefinitionSchema subscriptionDefinition)
        {
            string url = Settings.FullApiPath + "/appManagement/subscriptions";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() } 
            };

            var content = JsonSerializer.Serialize(subscriptionDefinition, options);

            var response = await Client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

            var json = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("CreateSubscriptionAsync ERROR");
            _logger.LogInformation(json);

            response.EnsureSuccessStatusCode();

            return json.ToString();
        }

        public async Task<RegistrationSchema> GetRegistrationAsync(string apiRegistrationId, string tenant)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            options.Converters.Add(new JsonStringEnumConverter());

            HttpRequestMessage request = new HttpRequestMessage();

            request.Headers.Add("engineTenantName", tenant);
            request.RequestUri = new Uri(Settings.FullApiPath + "/registrations/" + apiRegistrationId + "?includeInteractionsAndObjectives=true&includeRuntime=true&includeChildResults=true");
            request.Method = HttpMethod.Get;

            var response = await Client.SendAsync(request);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch(Exception e)
            {
                _logger.LogError("GetRegistrationAsync ERROR");
                _logger.LogError(await response.Content.ReadAsStringAsync());
            }
            

            var json = await response.Content.ReadAsStringAsync();
            RegistrationSchema registrations = JsonSerializer.Deserialize<RegistrationSchema>(json, options);

            return registrations;
        }

    }
}
