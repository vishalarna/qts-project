using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class DiscussionQuestionMap : Common.CommonMap<DiscussionQuestion>
    {
        public override void Configure(EntityTypeBuilder<DiscussionQuestion> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILATraineeEvaluation).WithMany(m => m.DiscussionQuestions).HasForeignKey(k => k.ILATraineeEvaluationId).IsRequired();
            builder.Property(o => o.QuestionText).IsRequired().HasMaxLength(200);
        }
    }
}
