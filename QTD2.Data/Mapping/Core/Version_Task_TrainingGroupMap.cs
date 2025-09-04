using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_Task_TrainingGroupMap : Common.CommonMap<Version_Task_TrainingGroup>
    {
        public override void Configure(EntityTypeBuilder<Version_Task_TrainingGroup> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Version_TaskId).IsRequired();
            builder.Property(o => o.Version_TrainingGroupId).IsRequired();
            builder.HasOne(o => o.Version_Task).WithMany(m => m.Version_Task_TrainingGroups).HasForeignKey(k => k.Version_TaskId).IsRequired();
            builder.HasOne(o => o.Version_TrainingGroup).WithMany(m => m.Version_Task_TrainingGroups).HasForeignKey(k => k.Version_TrainingGroupId).IsRequired();
            builder.HasIndex(i => new { i.Version_TrainingGroupId, i.Version_TaskId }).IsUnique();
        }
    }
}
