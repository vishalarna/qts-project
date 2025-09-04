using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class ClientSettings_FeatureMap : Common.CommonMap<ClientSettings_Feature>
    {
        public override void Configure(EntityTypeBuilder<ClientSettings_Feature> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.Feature).IsRequired().HasMaxLength(200);
            builder.Property(o => o.Enabled).IsRequired();
        }
    }
}
