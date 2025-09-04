using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QTD2.Data.Mapping.Core
{
    public class ReportSkeleton_CategoryMap : Common.CommonMap<QTD2.Domain.Entities.Core.ReportSkeleton_Category>
    {
        public override void Configure(EntityTypeBuilder<QTD2.Domain.Entities.Core.ReportSkeleton_Category> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired();
        }
    }
}
