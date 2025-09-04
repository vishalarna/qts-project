using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class ReportSkeletonColumnMap : Common.CommonMap<QTD2.Domain.Entities.Core.ReportSkeletonColumn>
    {
        public override void Configure(EntityTypeBuilder<QTD2.Domain.Entities.Core.ReportSkeletonColumn> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ReportSkeletonId).IsRequired().HasMaxLength(200);
            builder.Property(o => o.ColumnName).IsRequired();
        }
    }
}
