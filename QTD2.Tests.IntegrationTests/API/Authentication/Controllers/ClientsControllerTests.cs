using QTD2.Infrastructure.Identity.Settings;
using QTD2.Test.Data.Infrastructure.Identity.Settings;
using QTD2.Tests.IntegrationTests.Testing.Base;
using QTD2.Tests.IntegrationTests.Testing.Fixures;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace QTD2.Tests.IntegrationTests.API.Authentication.Controllers
{
    [Collection("Authentication Collection")]
    public class ClientsControllerTests : AuthenticationControllerBaseClass
    {
        public ClientsControllerTests(AuthenticationFixture authenticationFixture) : base(authenticationFixture)
        {
        }

        public static IEnumerable<object[]> Data_ClientsController_GetClients()
        {
            var data = new List<object[]>();

            data.Add(new object[] { null, null, HttpStatusCode.Unauthorized });
            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        [Theory]
        [MemberData(nameof(Data_ClientsController_GetClients))]
        public async Task ClientsController_GetClients(ClaimsBuilderOptions options, string username, HttpStatusCode statusCode)
        {
            string url = "/Clients";

            await TestGetAsync(url, username, options, statusCode);
        }
    }
}
