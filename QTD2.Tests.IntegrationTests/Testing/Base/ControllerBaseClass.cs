using QTD2.Infrastructure.Identity.Settings;
using QTD2.Tests.IntegrationTests.Testing.Fixures;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace QTD2.Tests.IntegrationTests.Testing.Base
{
    public class ControllerBaseClass<T> where T : class
    {
        public static string FullRightsUser = "michael@qts.com";
        public static string QTDAdminUser = "qtdadmin@qualitytrainingsystems.com";
        public static string QTDUser = "qtd@qualitytrainingsystems.com";
        protected readonly IFixture<T> _fixture;

        public ControllerBaseClass(IFixture<T> fixture)
        {
            _fixture = fixture;
        }

        public async Task<string> TestGetAsync(string url, string username, ClaimsBuilderOptions options, HttpStatusCode statusCode)
        {
            var client = await createClientAsync(username, options);

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal(statusCode, response.StatusCode);

            return content;
        }

        public async Task TestPostAsync(string url, string username, ClaimsBuilderOptions options, object model, HttpStatusCode statusCode)
        {
            HttpClient client = await createClientAsync(username, options);

            string json = getJson(model);
            var requestContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(statusCode, response.StatusCode);
            //Assert.Equal(content, responseContent);
        }

        public async Task TestPutAsync(string url, string username, ClaimsBuilderOptions options, object model, HttpStatusCode statusCode)
        {
            HttpClient client = await createClientAsync(username, options);

            string json = getJson(model);
            var requestContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(statusCode, response.StatusCode);
        }

        public async Task TestDeleteAsync(string url, string username, ClaimsBuilderOptions options, HttpStatusCode statusCode)
        {
            HttpClient client = await createClientAsync(username, options);

            var response = await client.DeleteAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(statusCode, response.StatusCode);
        }

        protected async Task<HttpClient> createClientAsync(string username, ClaimsBuilderOptions options)
        {
            await _fixture.StartupAsync();

            HttpClient client = new HttpClient();

            if (options != null && !string.IsNullOrEmpty(username))
            {
                client = await _fixture.Factory.CreateClientWithTestAuthAsync(options, username);
            }
            else
            {
                client = _fixture.Factory.CreateClient();
            }

            return client;
        }

        protected string getJson(object model)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(model);
        }
    }
}
