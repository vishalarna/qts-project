using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QTD2.Data.Mapping.Core
{
    public class ReportSkeleton_SubcategoryMap : Common.CommonMap<QTD2.Domain.Entities.Core.ReportSkeleton_Subcategory>
    {
        public override void Configure(EntityTypeBuilder<QTD2.Domain.Entities.Core.ReportSkeleton_Subcategory> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.Order).IsRequired();
            builder.HasOne(o => o.ReportSkeleton_Category).WithMany(m => m.ReportSkeleton_Subcategories).HasForeignKey(f => f.ReportSkeleton_CategoryId).IsRequired();
        }
    }
}
