using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class ReportMap : Common.CommonMap<Report>
    {
        public override void Configure(EntityTypeBuilder<Report> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.ClientUserId).IsRequired();
            builder.Property(o => o.ReportSkeletonId).IsRequired();
            builder.Property(o => o.OfficialReportTitle).IsRequired();
            builder.Property(o => o.InternalReportTitle).IsRequired();
        }

    }
}
