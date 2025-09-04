using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class ClassScheduleMap : Common.CommonMap<ClassSchedule>
    {
        public override void Configure(EntityTypeBuilder<ClassSchedule> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.ILA).WithMany(m => m.ClassSchedules).HasForeignKey(y => y.ILAID);
            builder.HasOne(x => x.Provider).WithMany(m => m.ClassSchedules).HasForeignKey(y => y.ProviderID);
            builder.HasOne(x => x.Instructor).WithMany(m => m.ClassSchedules).HasForeignKey(y => y.InstructorId);
            builder.HasOne(x => x.Location).WithMany(m => m.ClassSchedules).HasForeignKey(y => y.LocationId);
            builder.HasMany<ClassSchedule>(x => x.Recurrences).WithOne().HasForeignKey(f => f.RecurrenceId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.Property(p => p.IsRecurring).HasDefaultValue(false);
        }
    }
}
