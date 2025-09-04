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
    public class TrainingIssueMap : Common.CommonMap<TrainingIssue>
    {
        public override void Configure(EntityTypeBuilder<TrainingIssue> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.IssueID).IsRequired();
            builder.Property(o => o.IssueTitle).IsRequired();
            builder.Property(o => o.Description);
            builder.Property(o => o.TrainingIssueCreatedDate).IsRequired();
            builder.Property(o => o.DueDate).IsRequired();
            builder.Property(o => o.StatusId).IsRequired();
            builder.Property(o => o.SeverityId).IsRequired();
            builder.Property(o => o.DriverTypeId);
            builder.Property(o => o.DriverSubTypeId);
            builder.Property(o => o.OtherComments);
            builder.Property(o => o.DataElementId);
            builder.Property(o => o.PlannedResponse);
            builder.Property(o => o.TaskReviewId);
            builder.HasOne(o => o.Status).WithMany().HasForeignKey(k => k.StatusId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Severity).WithMany().HasForeignKey(k => k.SeverityId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.DriverType).WithMany().HasForeignKey(k => k.DriverTypeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.DriverSubType).WithMany().HasForeignKey(k => k.DriverSubTypeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.DataElement).WithOne(o=>o.TrainingIssue).HasForeignKey<TrainingIssue>(k => k.DataElementId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
