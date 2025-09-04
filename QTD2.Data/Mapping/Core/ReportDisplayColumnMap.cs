using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class ReportDisplayColumnMap : Common.CommonMap<ReportDisplayColumn>
    {
        public override void Configure(EntityTypeBuilder<ReportDisplayColumn> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.ColumnName).IsRequired();
            builder.Property(o => o.Display).IsRequired();
        }
    }
}
