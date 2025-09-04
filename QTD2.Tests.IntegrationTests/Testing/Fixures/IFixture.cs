namespace QTD2.Tests.IntegrationTests.Testing.Fixures
{
    public interface IFixture<T> where T : class
    {
        public TestApplicationFactory<T> Factory { get; }
        System.Threading.Tasks.Task StartupAsync();
    }
}
