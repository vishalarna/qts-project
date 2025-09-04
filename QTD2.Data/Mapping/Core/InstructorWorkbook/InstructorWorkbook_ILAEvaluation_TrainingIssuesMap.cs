using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
  public  class InstructorWorkbook_ILAEvaluation_TrainingIssuesMap : Common.CommonMap<InstructorWorkbook_ILAEvaluation_TrainingIssues>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILAEvaluation_TrainingIssues> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.IssueTitle).IsRequired();

            builder.Property(o => o.IssueDescription).IsRequired();
            builder.Property(o => o.FeedbackType).IsRequired();
            builder.Property(o => o.Severity).IsRequired();
        }

    }
}
