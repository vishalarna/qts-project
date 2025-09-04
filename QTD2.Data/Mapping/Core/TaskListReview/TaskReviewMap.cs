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
  public  class TaskReviewMap : Common.CommonMap<TaskReview>
    {
        public override void Configure(EntityTypeBuilder<TaskReview> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TaskListReviewId).IsRequired();
            builder.Property(o => o.TaskId).IsRequired();
            builder.Property(o => o.StatusId).IsRequired();
            builder.Property(o => o.ReviewDate);
            builder.Property(o => o.FindingId);
            builder.Property(o => o.RequalificationDueDate);
            builder.Property(o => o.Notes);
            builder.Property(o => o.TrainingIssueId);
            builder.HasOne(o => o.TaskListReview).WithMany(o=>o.TaskReviews).HasForeignKey(k => k.TaskListReviewId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Status).WithMany().HasForeignKey(k => k.StatusId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Task).WithMany(o=>o.TaskReviews).HasForeignKey(k => k.TaskId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Finding).WithMany().HasForeignKey(k => k.FindingId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Finding).WithMany().HasForeignKey(k => k.FindingId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.TrainingIssue).WithOne().HasForeignKey<TaskReview>(k => k.TrainingIssueId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
