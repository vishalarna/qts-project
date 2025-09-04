using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClassScheduleHistoryMap : Common.CommonMap<ClassScheduleHistory>
    {
        public override void Configure(EntityTypeBuilder<ClassScheduleHistory> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.ClassSchedule).WithMany(m => m.ClassScheduleHistories).HasForeignKey(y => y.ClassScheduleID);
           
        }
    }
}
