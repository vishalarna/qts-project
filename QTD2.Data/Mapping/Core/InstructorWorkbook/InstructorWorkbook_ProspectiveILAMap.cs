using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ProspectiveILAMap : Common.CommonMap<InstructorWorkbook_ProspectiveILA>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ProspectiveILA> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Provider).WithMany().HasForeignKey(o => o.ProviderId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.Instructor).WithMany().HasForeignKey(o => o.InstructorId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.Instructor1).WithMany().HasForeignKey(o => o.ReviewerId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.Property(o => o.ILAName).IsRequired();
            builder.Property(o => o.ILANumber).IsRequired();
            builder.Property(o => o.Result);
            builder.Property(o => o.ProblemStatementResponse);
            builder.Property(o => o.GoalsResponse);
            builder.Property(o => o.ResultsResponse);
            builder.Property(o => o.PerformanceObjectivesResponse);
            builder.Property(o => o.PerformanceMetricResponse);
            builder.Property(o => o.KnowledgeResponse);
            builder.Property(o => o.LearningMetricResponse);
            builder.Property(o => o.PerceptionResponse);
            builder.Property(o => o.MotivationResponse);
            builder.Property(o => o.NtrComments);
            builder.Property(o => o.CreatorComments);
            builder.Property(o => o.AcceptNotes);

        }

    }
}
