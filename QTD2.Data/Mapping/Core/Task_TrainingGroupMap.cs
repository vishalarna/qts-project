using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Task_TrainingGroupMap : Common.CommonMap<Task_TrainingGroup>
    {
        public override void Configure(EntityTypeBuilder<Task_TrainingGroup> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TaskId).IsRequired();
            builder.Property(o => o.TrainingGroupId).IsRequired();
            builder.HasOne(o => o.Task).WithMany(m => m.Task_TrainingGroups).HasForeignKey(k => k.TaskId).IsRequired();
            builder.HasOne(o => o.TrainingGroup).WithMany(m => m.Task_TrainingGroups).HasForeignKey(k => k.TrainingGroupId).IsRequired();
            builder.HasIndex(i => new { i.TrainingGroupId, i.TaskId }).IsUnique();
        }
    }
}
