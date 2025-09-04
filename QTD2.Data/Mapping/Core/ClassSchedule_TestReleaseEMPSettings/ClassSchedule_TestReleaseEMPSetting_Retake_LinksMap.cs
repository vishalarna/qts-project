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
    public class ClassSchedule_TestReleaseEMPSetting_Retake_LinksMap : Common.CommonMap<ClassSchedule_TestReleaseEMPSetting_Retake_Link>
    {
        public override void Configure(EntityTypeBuilder<ClassSchedule_TestReleaseEMPSetting_Retake_Link> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ClassSchedule_TestReleaseSettingId).IsRequired();
            builder.Property(o => o.RetakeTestId).IsRequired();
            builder.HasOne(o => o.RetakeTest).WithMany(w => w.ClassSchedule_TestReleaseEMPSetting_RetakeLinks).HasForeignKey(f => f.RetakeTestId).OnDelete(DeleteBehavior.NoAction); ;
            builder.HasOne(o => o.ClassSchedule_TestReleaseEMPSetting).WithMany(o => o.ClassSchedule_TestReleaseEMPSetting_RetakeLinks).HasForeignKey(k => k.ClassSchedule_TestReleaseSettingId).OnDelete(DeleteBehavior.NoAction); 
        }
    }
}