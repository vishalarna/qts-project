using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class ReportSkeletonMap : Common.CommonMap<QTD2.Domain.Entities.Core.ReportSkeleton>
    {
        public override void Configure(EntityTypeBuilder<QTD2.Domain.Entities.Core.ReportSkeleton> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.DefaultTitle).IsRequired().HasMaxLength(200);
        }
    }
}
