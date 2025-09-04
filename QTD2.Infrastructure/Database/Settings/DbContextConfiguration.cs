namespace QTD2.Infrastructure.Database.Settings
{
    public class DbContextConfiguration
    {
        public DbContextNames Name { get; set; }

        public SupportedProviders Provider { get; set; }

        public string ConnectionString { get; set; }
    }
}
