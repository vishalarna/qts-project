using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SelfRegistrationOptionsMap : Common.CommonMap<ILA_SelfRegistrationOptions>
    {
        public override void Configure(EntityTypeBuilder<ILA_SelfRegistrationOptions> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithOne(w => w.ILA_SelfRegistrationOption).HasForeignKey<ILA_SelfRegistrationOptions>(f => f.ILAId).IsRequired();
        }
    }
}
