using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class NERCTargetAudienceMaps : Common.CommonMap<NERCTargetAudience>
    {
        public override void Configure(EntityTypeBuilder<NERCTargetAudience> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(200);
            builder.Property(o => o.IsOther).HasDefaultValue(false);
            builder.Property(o => o.OtherName).HasMaxLength(200);
        }
    }
}
