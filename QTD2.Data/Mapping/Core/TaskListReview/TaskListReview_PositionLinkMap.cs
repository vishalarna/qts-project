using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class TaskListReview_PositionLinkMap : Common.CommonMap<TaskListReview_PositionLink>
    {
        public override void Configure(EntityTypeBuilder<TaskListReview_PositionLink> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.TaskListReview).WithMany(m => m.TaskListReview_PositionLinks).HasForeignKey(k => k.TaskListReviewId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Position).WithMany(m=>m.TaskListReview_PositionLinks).HasForeignKey(k => k.PositionId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
