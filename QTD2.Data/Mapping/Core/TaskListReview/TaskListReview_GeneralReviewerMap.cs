using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TaskListReview_GeneralReviewerMap : Common.CommonMap<TaskListReview_GeneralReviewer>
    {
        public override void Configure(EntityTypeBuilder<TaskListReview_GeneralReviewer> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TaskListReviewId).IsRequired();
            builder.Property(o => o.GeneralReviewerId).IsRequired();
            builder.HasOne(o => o.TaskListReview).WithMany(m => m.GeneralReviewers).HasForeignKey(k => k.TaskListReviewId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.GeneralReviewer).WithMany().HasForeignKey(k => k.GeneralReviewerId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
