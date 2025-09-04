using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Authentication
{
    public class InstanceIdentityProviderLink : Common.Entity
    {
        public int InstanceId { get; set; }
        public int IdentityProviderId { get; set; }
        public virtual Instance Instance { get; set; }
        public virtual IdentityProvider IdentityProvider { get; set; }
        public InstanceIdentityProviderLink() { }
        public InstanceIdentityProviderLink(int instanceId,int identityProviderId)
        {
            InstanceId = instanceId;
            IdentityProviderId = identityProviderId;
        }

    }
}
