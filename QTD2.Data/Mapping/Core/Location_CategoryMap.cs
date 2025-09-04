using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class  Location_CategoryMap : Common.CommonMap<Location_Category>
    {
        public override void Configure(EntityTypeBuilder<Location_Category> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.LocCategoryDesc);
            builder.Property(o => o.LocCategoryTitle).HasMaxLength(200).IsRequired();
            builder.Property(o => o.EffectiveDate).IsRequired();
        }
    }
}
