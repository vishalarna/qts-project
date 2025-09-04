using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Authentication
{
    public class InstanceIdentityProviderLinkMap : Common.CommonMap<InstanceIdentityProviderLink>
    {
        public override void Configure(EntityTypeBuilder<InstanceIdentityProviderLink> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Instance).WithMany(p => p.InstanceIdentityProviderLinks).HasForeignKey(k => k.InstanceId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.IdentityProvider).WithMany(p => p.InstanceIdentityProviderLinks).HasForeignKey(k => k.IdentityProviderId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
