using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.HttpClients
{
    public class MockQtdAuthenticationServiceHandler : HttpMessageHandler
    {
        string _baseUrl;

        public MockQtdAuthenticationServiceHandler(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string requestUri = request.RequestUri.ToString().Split(_baseUrl)[1].Split("?")[0];
            switch (requestUri)
            {
                case "/instances/QTD2/settings":
                    return doGetInstances(request);
                default:
                    throw new NotImplementedException();
            }
        }

        private async Task<HttpResponseMessage> doGetInstances(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            //string requestContent = await request.Content.ReadAsStringAsync();
            string jsonString = "{\"InstanceId\": \"1\" , \"DatabaseName\": \"QTD2\" ,\"DataBaseVersion\":\"2\"}";
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            response.Content = content;
            return response;
        }
    }
}
