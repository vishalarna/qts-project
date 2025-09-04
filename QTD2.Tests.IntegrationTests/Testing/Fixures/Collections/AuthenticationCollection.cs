using Xunit;

namespace QTD2.Tests.IntegrationTests.Testing.Fixures
{
    [CollectionDefinition("Authentication Collection")]
    public class AuthenticationCollection : ICollectionFixture<AuthenticationFixture>
    {
    }
}
