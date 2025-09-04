using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.Dynamic;
using System.Text.Json;

namespace QTD2.Infrastructure.HttpClients
{
    //https://rustici-docs.s3.amazonaws.com/engine/22.x/api/apiV2.html#/
    public class MockScormHandler : HttpMessageHandler
    {
        string _baseUrl;

        public MockScormHandler(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string requestUri = setRequestUri(request);
            switch (requestUri)
            {
                case "/ping":
                    return doPing(request);
                case "/courses/upload":
                    return doCreateUploadAndImportCourseJobAsync(request);
                case "/registrations/withLaunchLink":
                    return doCreateRegistrationWithLaunchLinkAsync(request);
                case "handlePostAppConfiguration":
                    return doSetApplicationConfigurationAsync(request);
                case "handleGetAppConfiguration":
                    return doGetApplicationConfigurationAsync(request);
                case "handleCreateOrUpdateTenant":
                    return doCreateOrUpdateTenantAsync(request);
                case "/appManagement/subscriptions":
                    return doCreateSubscriptionAsync(request);
                case "handleGetSusbscriptions":
                    return doGetSubscriptionsAsync(request);
                default:
                    throw new NotImplementedException();
            }
        }

        private string setRequestUri(HttpRequestMessage request)
        {
            string requestUri = request.RequestUri.ToString().Split(_baseUrl)[1].Split("?")[0];
            if (requestUri == "/appManagement/configuration")
            {
                if (request.Method == HttpMethod.Get)
                {
                    return "handleGetAppConfiguration";
                }
                else
                {
                    return "handlePostAppConfiguration";
                }
            } 
            else if (requestUri.Contains("/appManagement/tenants"))
            {
                return "handleCreateOrUpdateTenant";
            }
            else if (requestUri.Contains("/appManagement/subscriptions"))
            {
                return "handleGetSusbscriptions";
            }
            return requestUri;
        }
        private Task<HttpResponseMessage> doPing(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            dynamic content = new ExpandoObject();

            content.apiMessage = "Some API Message";
            content.currentTime = DateTime.Now.Ticks.ToString();
            content.databaseMessage = "Some Db Message";

            response.Content = new StringContent(JsonSerializer.Serialize(content));

            return Task.FromResult(response);
        }

        private async Task<HttpResponseMessage> doCreateUploadAndImportCourseJobAsync(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            System.IO.Stream stream = await request.Content.ReadAsStreamAsync();
            string jsonString = "{\"result\":\"c83f9db2-1e63-4a92-bbdd-8b37a20a0d0a\"}";
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            response.Content = content;
            return response;
        }

        private async Task<HttpResponseMessage> doCreateRegistrationWithLaunchLinkAsync(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            string requestContent = await request.Content.ReadAsStringAsync();
            string jsonString = "{\"launchLink\":\"https://www.google.com\"}";

            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            response.Content = content;

            return response;
        }

        private async Task<HttpResponseMessage> doCreateOrUpdateTenantAsync(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            string requestContent = await request.Content.ReadAsStringAsync();
            string jsonString = "success";

            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            response.Content = content;

            return response;
        }

        private async Task<HttpResponseMessage> doSetApplicationConfigurationAsync(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            string requestContent = await request.Content.ReadAsStringAsync();
            string jsonString = "success";

            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            response.Content = content;

            return response;
        }

        private async Task<HttpResponseMessage> doGetApplicationConfigurationAsync(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            string requestContent = await request.Content.ReadAsStringAsync();
            string jsonString = "{\"settingItems\": [{\"id\": \"string\",\"effectiveValue\": \"string\",\"effectiveValueSource\": \"default\",\"explicitValue\": \"string\",\"metadata\": {\"default\": \"string\",\"dataType\": \"string\",\"settingDescription\": \"string\",\"level\": \"string\",\"learningStandards\": [\"string\"],\"fallback\": \"string\",\"validValues\": [{\"value\": \"string\",\"valueDescription\": \"string\"}]}}]}";

            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            response.Content = content;

            return response;
        }

        private async Task<HttpResponseMessage> doCreateSubscriptionAsync(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            string requestContent = await request.Content.ReadAsStringAsync();
            string jsonString = "{\"result\":\"c83f9db2-1e63-4a92-bbdd-8b37a20a0d0a\"}";
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            response.Content = content;
            return response;
        }

        private async Task<HttpResponseMessage> doGetSubscriptionsAsync(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            //string requestContent = await request.Content.ReadAsStringAsync();
            string jsonString = "{\"subscriptions\": [\n {\"id\": \"string\", \"lastUpdate\": \"2023-08-14T09:52:28.525Z\",\"definition\": { \"topic\": \"CourseImport\",\"subtopics\": [\n          \"string\"\n        ],\n        \"enabled\": true,\n        \"url\": \"string\",\n        \"authId\": \"string\",\n        \"timeoutMS\": 0,\n        \"asyncMode\": \"Thread\",\n        \"strictOrdering\": \"false\",\n        \"expiresMS\": 0,\n        \"retryAttempts\": 0,\n        \"retryDelaySeconds\": 0,\n        \"ignoreBeforeDate\": \"2023-08-14T09:52:28.525Z\",\n        \"filters\": [\n          {\n            \"target\": \"CourseId\",\n            \"matches\": [\n              \"string\"\n            ]\n          }\n        ]\n      },\n      \"lastExceptionReference\": \"string\",\n      \"lastExceptionDate\": \"2023-08-14T09:52:28.525Z\"\n    }\n  ],\n  \"more\": \"string\"\n}";
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            response.Content = content;
            return response;
        }
    }
}
