namespace QTD2.Infrastructure.Model.Instance
{
    public class InstanceCreateOptions
    {
        public string ClientName { get; set; }

        public string Name { get; set; }

        public bool CreateDatabase { get; set; }
        public string DatabaseName { get; set; }
        public bool IsInBeta { get; set; }
        public int ClientAccountNumber { get; set; }
        public string ScormTenant { get; set; }
        public int IdentityProviderId { get; set; }
        public bool? MFAEnabled { get; set; }
    }
}
