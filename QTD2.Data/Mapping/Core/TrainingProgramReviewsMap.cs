using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TrainingProgramReviewsMap : Common.CommonMap<TrainingProgramReview>
    {
        public override void Configure(EntityTypeBuilder<TrainingProgramReview> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.TrainingProgram).WithMany(s => s.TrainingProgramReviews).HasForeignKey(y => y.TrainingProgramId).IsRequired();
            builder.Property(o => o.ReviewDate);
            builder.Property(o => o.StartDate);
            builder.Property(o => o.EndDate);
            builder.Property(o => o.Purpose);
            builder.Property(o => o.Method);
            builder.Property(o => o.HistoricalBackground);
            builder.Property(o => o.ProgramDesign);
            builder.Property(o => o.ProgramMaterials);
            builder.Property(o => o.ProgramImplementation);
            builder.Property(o => o.EvaluationOfTraineeLearning);
            builder.Property(o => o.StudentEvaluationResults);
            builder.Property(o => o.Conclusion);
            builder.Property(o => o.Summary);
            builder.Property(o => o.TrainerName);
            builder.Property(o => o.Title);
            builder.Property(o => o.TrainerSignOff);
            builder.Property(o => o.Published).IsRequired();
        }
    }
}
