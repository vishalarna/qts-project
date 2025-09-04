using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QTD2.Data.Mapping.Core
{
    public class ReportSkeleton_Subcategory_ReportMap : Common.CommonMap<QTD2.Domain.Entities.Core.ReportSkeleton_Subcategory_Report>
    {
        public override void Configure(EntityTypeBuilder<QTD2.Domain.Entities.Core.ReportSkeleton_Subcategory_Report> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Order).IsRequired();
            builder.HasOne(o => o.ReportSkeleton_Subcategory).WithMany(m => m.ReportSkeleton_Subcategory_Reports).HasForeignKey(f => f.ReportSkeleton_SubcategoryId).IsRequired();
            builder.HasOne(o => o.ReportSkeleton).WithMany().HasForeignKey(f => f.ReportSkeletonId).IsRequired();
        }
    }
}
