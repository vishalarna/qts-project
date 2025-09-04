using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
   public class CBT_ScormRegistrationMap : Common.CommonMap<CBT_ScormRegistration>
    {
        public override void Configure(EntityTypeBuilder<CBT_ScormRegistration> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ScormUpload).WithMany(e => e.CBT_ScormRegistration).HasForeignKey(k => k.CBTScormUploadId).IsRequired();
            builder.HasOne(o => o.ClassScheduleEmployee).WithMany(o => o.ScormRegistrations).HasForeignKey(o => o.ClassScheduleEmployeeId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(i => new { i.ClassScheduleEmployeeId}).IsUnique().HasFilter("[Active] = 1");
            builder.Property(p => p.TotalTime).HasConversion<long>();
        }
    }
}
