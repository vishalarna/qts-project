using QTD2.API.Authentication;
using QTD2.Tests.IntegrationTests.Testing.Fixures;

namespace QTD2.Tests.IntegrationTests.Testing.Base
{
    public class AuthenticationControllerBaseClass : ControllerBaseClass<Startup>
    {
        public AuthenticationControllerBaseClass(AuthenticationFixture authenticationFixture) : base(authenticationFixture)
        {

        }
    }
}
