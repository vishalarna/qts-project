using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Authentication
{
    public class InstanceSetting : Entity
    {
        public int InstanceId { get; set; }

        public string DatabaseName { get; set; }

        public string DataBaseVersion { get; set; }
        public bool? MFAEnabled { get; set; }
        public virtual Instance Instance { get; set; }

        public string ScormTenant { get; set; }
        public int ClientAccountNumber { get; set; }
        public int? DefaultIdentityProviderId { get; set; }
        public string PublicUrl {  get; set; }
        public virtual IdentityProvider DefaultIdentityProvider { get; set; }

        public InstanceSetting(int instanceId, string databasename, string databaseVersion)
        {
            InstanceId = instanceId;
            DatabaseName = databasename;
            DataBaseVersion = databaseVersion;
        }

        public InstanceSetting()
        {
        }

        private void UpdateVersion(string version)
        {
        }
    }
}
