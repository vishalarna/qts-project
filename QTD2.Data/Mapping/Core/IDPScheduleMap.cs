using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class IDPScheduleMap : Common.CommonMap<IDPSchedule>
    {
        public override void Configure(EntityTypeBuilder<IDPSchedule> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.startDate);
            builder.Property(p => p.endDate);
            builder.Property(p => p.plannedDate);
            builder.HasOne(o => o.IDP).WithMany(m => m.IDPSchedules).HasForeignKey(f => f.IDPId).IsRequired();
            builder.HasOne(o => o.ClassSchedule).WithMany(m => m.IDPSchedules).HasForeignKey(f => f.ClassScheduleId).IsRequired();
        }
    }
}
