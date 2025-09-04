using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class ClassSchedule_Roster_TimeRecordMap : Common.CommonMap<ClassSchedule_Roster_TimeRecord>
    {
        public override void Configure(EntityTypeBuilder<ClassSchedule_Roster_TimeRecord> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.StartDateTime).IsRequired();
            builder.Property(o => o.EndDateTime).IsRequired();
            builder.Property(o => o.Sequence).IsRequired();
            builder.HasOne(o => o.ClassSchedule_Roster).WithMany(o => o.TimeRecords).HasForeignKey(k => k.ClassSchedule_RosterId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
