using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_PositionMap : Common.CommonMap<Version_Position>
    {
        public override void Configure(EntityTypeBuilder<Version_Position> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.PositionId).IsRequired();
            builder.Property(o => o.PositionTitle).IsRequired().HasMaxLength(200);
            builder.Property(o => o.PositionNumber);
            builder.Property(o => o.PositionDescription).HasMaxLength(2000);
            builder.Property(o => o.HyperLink).HasMaxLength(400);
            builder.Property(o => o.PositionsFileUpload);
            builder.Property(o => o.IsPublished);

            builder.HasOne(o => o.Position).WithMany(x =>x.Version_Positions).HasForeignKey(f => f.PositionId).IsRequired();
        }
    }
}
