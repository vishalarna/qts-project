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
    public class ClassSchedule_TestReleaseEMPSettingsMap : Common.CommonMap<ClassSchedule_TestReleaseEMPSetting>
    {
        public override void Configure(EntityTypeBuilder<ClassSchedule_TestReleaseEMPSetting> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ClassScheduleId).IsRequired();
            builder.Property(o => o.UsePreTestAndTest);
            builder.Property(o => o.PreTestRequired);
            builder.Property(o => o.RetakeEnabled);
            builder.Property(o => o.FinalTestId);
            builder.Property(o => o.PreTestId);
            builder.Property(o => o.jobDetails);
            builder.Property(o => o.PreTestAvailableOnEnrollment);
            builder.Property(o => o.PreTestAvailableOneStartDate);
            builder.Property(o => o.PreTestScore);
            builder.Property(o => o.ShowStudentSubmittedPreTestAnswers);
            builder.Property(o => o.ShowCorrectIncorrectPreTestAnswers);
            builder.Property(o => o.MakeAvailableBeforeDays);
            builder.Property(o => o.MakeAvailableBeforeWeeks);
            builder.Property(o => o.DaysOrWeeks);
            builder.Property(o => o.FinalTestPassingScore);
            builder.Property(o => o.MakeFinalTestAvailableImmediatelyAfterStartDate);
            builder.Property(o => o.MakeFinalTestAvailableOnClassEndDate);
            builder.Property(o => o.MakeFinalTestAvailableAfterCBTCompleted);
            builder.Property(o => o.MakeFinalTestAvailableOnSpecificTime);
            builder.Property(o => o.FinalTestSpecificTimePrior);
            builder.Property(o => o.FinalTestDueDate);
            builder.Property(o => o.ShowStudentSubmittedFinalTestAnswers);
            builder.Property(o => o.ShowStudentSubmittedRetakeTestAnswers);
            builder.Property(o => o.ShowCorrectIncorrectFinalTestAnswers);
            builder.Property(o => o.ShowCorrectIncorrectRetakeTestAnswers);
            builder.Property(o => o.AutoReleaseRetake);
            builder.Property(o => o.NumberOfRetakes);
            builder.Property(o => o.EmpSettingsReleaseTypeId);
            builder.HasOne(o => o.ClassSchedule).WithOne(m => m.ClassSchedule_TestReleaseEMPSettings).HasForeignKey<ClassSchedule_TestReleaseEMPSetting>(f => f.ClassScheduleId).IsRequired();
            builder.HasOne(o => o.FinalTest).WithMany().HasForeignKey(f => f.FinalTestId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.PreTest).WithMany(x => x.ClassSchedule_TestReleaseEMPSettings).HasForeignKey(f => f.PreTestId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.EmpSettingsReleaseType).WithMany().HasForeignKey(f => f.EmpSettingsReleaseTypeId);
        }
    }
}