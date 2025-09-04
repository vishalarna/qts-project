using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class PositionMap : Common.CommonMap<Position>
    {
        public override void Configure(EntityTypeBuilder<Position> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.PositionTitle).IsRequired().HasMaxLength(200);
            builder.Property(o => o.PositionNumber);
            builder.Property(o => o.PositionAbbreviation);
            builder.Property(o => o.PositionDescription);
            builder.Property(o => o.HyperLink).HasMaxLength(400);
            builder.Property(o => o.PositionsFileUpload);
            builder.Property(o => o.IsPublished);
        }
    }
}
