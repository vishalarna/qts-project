using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ILAImplement_ClassScheduleMap : Common.CommonMap<InstructorWorkbook_ILAImplement_ClassSchedule>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILAImplement_ClassSchedule> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.Property(o => o.StartDate);
            builder.Property(o => o.EndDate);
            builder.HasOne(o => o.Instructor).WithMany().HasForeignKey(o => o.InstructorId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.Location).WithMany().HasForeignKey(o => o.LocationId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.Test).WithMany().HasForeignKey(o => o.TestId).IsRequired();
            builder.HasOne(o => o.StudentEvaluationForm).WithMany().HasForeignKey(o => o.EvaluationId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.Property(o => o.IsInstructorEnrolled);
            builder.Property(o => o.IsTestLinked);
            builder.Property(o => o.IsRetakeLinked);
            builder.Property(o => o.RetakeTestId);
        }

    }
}
