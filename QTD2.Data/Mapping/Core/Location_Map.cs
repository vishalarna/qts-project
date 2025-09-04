using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Location_Map : Common.CommonMap<Location>
    {
        public override void Configure(EntityTypeBuilder<Location> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.LocNumber).HasMaxLength(50).IsRequired();
            builder.Property(o => o.LocName).HasMaxLength(200).IsRequired();
            builder.Property(o => o.EffectiveDate).IsRequired();
            builder.HasOne(o => o.Location_Category).WithMany(m => m.Locations).HasForeignKey(k => k.LocCategoryID).IsRequired();
        }
    }
}
