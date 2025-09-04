using System.Collections.Generic;

namespace QTD2.Domain.Entities.Authentication
{
    public class Instance : Common.Entity
    {
        public int ClientId { get; set; }

        public string Name { get; set; }

        public bool IsInBeta { get; set; }

        public virtual Client Client { get; set; }

        public virtual InstanceSetting InstanceSetting { get; set; }
        public virtual ICollection<InstanceIdentityProviderLink> InstanceIdentityProviderLinks { get; set; } = new List<InstanceIdentityProviderLink>();

        public Instance(int clientId, string name , bool isInBeta)
        {
            ClientId = clientId;
            Name = name;
            IsInBeta = isInBeta;
        }

        public Instance()
        {
        }

        private void UpdateVersion(string version)
        {
        }

        private void Update(string name)
        {
        }
    }
}
