using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Authentication
{
    public class IdentityProvider : Common.Entity
    {
        public string Name { get; set; }

        public string Type
        {
            get
            {
                return this.GetType().Name.Replace("Provider","").Trim();
            }
            set { }
        }
        public virtual ICollection<InstanceSetting> InstanceSettings { get; set; } = new List<InstanceSetting>();
        public virtual ICollection<InstanceIdentityProviderLink> InstanceIdentityProviderLinks { get; set; } = new List<InstanceIdentityProviderLink>();
        public IdentityProvider() { }
    }
}
