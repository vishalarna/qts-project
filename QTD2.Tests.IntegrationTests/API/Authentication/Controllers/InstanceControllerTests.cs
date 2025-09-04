
using QTD2.Infrastructure.Identity.Settings;
using QTD2.Tests.IntegrationTests.Testing.Fixures;
using Xunit;

namespace QTD2.Tests.IntegrationTests.API.Authentication.Controllers
{
    [Collection("Authentication Collection")]
    public class InstanceControllerTests
    {
        readonly AuthenticationFixture _authenticationFixture;

        public InstanceControllerTests(AuthenticationFixture authenticationFixture)
        {
            _authenticationFixture = authenticationFixture;
        }       
    }
}
