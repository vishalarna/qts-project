using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class RatingScaleMap : Common.CommonMap<RatingScale>
    {
        public override void Configure(EntityTypeBuilder<RatingScale> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Position1Text).IsRequired().HasMaxLength(50);
            builder.Property(o => o.Position2Text).IsRequired().HasMaxLength(50);
            builder.Property(o => o.Position3Text).HasMaxLength(50);
            builder.Property(o => o.Position4Text).HasMaxLength(50);
            builder.Property(o => o.Position5Text).HasMaxLength(50);
        }
    }
}
