using Xunit;

namespace QTD2.Tests.IntegrationTests.Testing.Fixures.Collections
{
    [CollectionDefinition("QTD Collection")]
    public class QTDCollection : ICollectionFixture<QTDFixture>
    {
    }
}
