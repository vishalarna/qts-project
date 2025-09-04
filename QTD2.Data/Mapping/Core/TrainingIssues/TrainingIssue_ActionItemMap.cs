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
    public class TrainingIssue_ActionItemMap : Common.CommonMap<TrainingIssue_ActionItem>
    {
        public override void Configure(EntityTypeBuilder<TrainingIssue_ActionItem> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TrainingIssueId).IsRequired();
            builder.Property(o => o.Order).IsRequired();
            builder.Property(o => o.ActionItemName).IsRequired();
            builder.Property(o => o.PriorityId);
            builder.Property(o => o.DateAssigned);
            builder.Property(o => o.DueDate);
            builder.Property(o => o.DateCompleted);
            builder.Property(o => o.StatusId);
            builder.Property(o => o.Notes);
            builder.Property(o => o.AssigneeName);
            builder.HasOne(o => o.TrainingIssue).WithMany(o => o.ActionItems).HasForeignKey(k => k.TrainingIssueId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Priority).WithMany().HasForeignKey(k => k.PriorityId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Status).WithMany().HasForeignKey(k => k.StatusId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
