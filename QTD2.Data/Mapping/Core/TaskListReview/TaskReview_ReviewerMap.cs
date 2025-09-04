using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
   public class TaskReview_ReviewerMap : Common.CommonMap<TaskReview_Reviewer>
    {
        public override void Configure(EntityTypeBuilder<TaskReview_Reviewer> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TaskReviewId).IsRequired();
            builder.Property(o => o.ReviewerId).IsRequired();
            builder.HasOne(o => o.TaskReview).WithMany(o => o.Reviewers).HasForeignKey(k => k.TaskReviewId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Reviewer).WithMany().HasForeignKey(k => k.ReviewerId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}