using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TrainingProgramReview_SupportingDocumentMap : Common.CommonMap<TrainingProgramReview_SupportingDocument>
    {
        public override void Configure(EntityTypeBuilder<TrainingProgramReview_SupportingDocument> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.TrainingProgramReviewId).IsRequired();
            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.Hyperlink).IsRequired();
            builder.Property(o => o.Number).IsRequired();

            builder.HasOne(o => o.TrainingProgramReview).WithMany(x => x.TrainingProgramReview_SupportingDocuments).HasForeignKey(y => y.TrainingProgramReviewId).IsRequired();
        }
    }
}
