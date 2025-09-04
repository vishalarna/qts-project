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
    public class TaskListReviewMap : Common.CommonMap<TaskListReview>
    {
        public override void Configure(EntityTypeBuilder<TaskListReview> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Title).IsRequired();
            builder.Property(o => o.TypeId).IsRequired();
            builder.Property(o => o.StartDate).IsRequired();
            builder.Property(o => o.EndDate).IsRequired();
            builder.Property(o => o.StatusId).IsRequired();
            builder.Property(o => o.Conclusion);
            builder.Property(o => o.ApprovalDate);
            builder.Property(o => o.Signature);
            builder.Property(o => o.ReviewedBy);
            builder.HasOne(o => o.Type).WithMany().HasForeignKey(k => k.TypeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Status).WithMany().HasForeignKey(k => k.StatusId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
