using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClassSchedule_TQEMPSettingMap : Common.CommonMap<ClassSchedule_TQEMPSetting>
    {
        public override void Configure(EntityTypeBuilder<ClassSchedule_TQEMPSetting> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TQRequired);
            builder.HasOne(o => o.ClassSchedule).WithOne(m => m.ClassSchedule_TQEMPSettings).HasForeignKey<ClassSchedule_TQEMPSetting>(f => f.ClassScheduleId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
