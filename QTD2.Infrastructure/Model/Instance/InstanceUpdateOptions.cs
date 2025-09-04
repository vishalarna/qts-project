namespace QTD2.Infrastructure.Model.Instance
{
    public class InstanceUpdateOptions
    {
        public string Name { get; set; }
        public string? ClientAccountNumber { get; set; }
        public bool IsInBeta { get; set; }
        public int IdentityProviderId { get; set; }
        public bool? MFAEnabled { get; set; }
    }
}
