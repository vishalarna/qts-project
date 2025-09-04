using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ILAEvaluationMap : Common.CommonMap<InstructorWorkbook_ILAEvaluation>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILAEvaluation> builder)
        {
            base.Configure(builder); builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.Property(o => o.StudentEvaluationResults);
            builder.Property(o => o.InstructorFeedback);
            builder.Property(o => o.Level1Status);
            builder.Property(o => o.Notes);
            builder.Property(o => o.Level2Status);
            builder.Property(o => o.OpenTextField);
            builder.Property(o => o.Level3Status);
            builder.Property(o => o.Level4TextField);
            builder.Property(o => o.Level4TextStatus);
            builder.Property(o => o.EvaluationResult);

        }

    }
}
