using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SaftyHazard_CategoryMap : Common.CommonMap<SaftyHazard_Category>
    {
        public override void Configure(EntityTypeBuilder<SaftyHazard_Category> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description);
            builder.Property(o => o.EffectiveDate);
            builder.Property(o => o.Number).IsRequired();
            builder.Property(o => o.Notes);
            builder.Property(o => o.Title).HasMaxLength(200).IsRequired();
        }
    }
}
