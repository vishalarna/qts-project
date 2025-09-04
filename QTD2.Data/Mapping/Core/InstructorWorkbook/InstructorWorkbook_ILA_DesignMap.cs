using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ILA_DesignMap : Common.CommonMap<InstructorWorkbook_ILA_Design>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILA_Design> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ILAId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.Property(o => o.IsOptionalText);
            builder.Property(o => o.DesignResult);
            builder.Property(o => o.OtherTypeAssessmentTool);
            builder.Property(o => o.ILADetailsStatus);
            builder.Property(o => o.ObjectivesAndSegmentsStatus);
            builder.Property(o => o.ProceduresStatus);
            builder.Property(o => o.TrainingPlanResponse);
            builder.Property(o => o.TrainingPlanStatus);
            builder.Property(o => o.EvaluationMethodResponse);
            builder.Property(o => o.EvaluationMethodResponseStatus);
            builder.Property(o => o.PrerequisitesStatus);
            builder.Property(o => o.ResourcesStatus);
            builder.Property(o => o.ILAApplicationStatus);
            builder.Property(o => o.PilotDataApplicable);
            builder.Property(o => o.InstructorComments);
            builder.Property(o => o.ReviewerComments);
        }
    }
}
