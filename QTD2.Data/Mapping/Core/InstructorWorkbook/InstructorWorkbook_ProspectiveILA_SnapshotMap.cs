using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    class InstructorWorkbook_ProspectiveILA_SnapshotMap : Common.CommonMap<InstructorWorkbook_ProspectiveILA_Snapshot>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ProspectiveILA_Snapshot> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.InstructorWorkbook_ProspectiveILA).WithMany().HasForeignKey(o => o.ProspectiveILAID).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.Property(o => o.Version);
            builder.Property(o => o.ProblemStatementResponse);
            builder.Property(o => o.GoalsResponse);
            builder.Property(o => o.ResultsResponse);
            builder.Property(o => o.PerformanceObjectivesResponse);
            builder.Property(o => o.KnowledgeResponse);
            builder.Property(o => o.PerformanceMetricResponse);
            builder.Property(o => o.LearningMetricResponse);
            builder.Property(o => o.PerceptionResponse);
            builder.Property(o => o.MotivationResponse);
        }

    }
}