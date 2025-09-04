using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{

    public class R5ImpactedTasksMap : Common.CommonMap<R5ImpactedTask>
    {
        public override void Configure(EntityTypeBuilder<R5ImpactedTask> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.PositionTaskId).IsRequired();
            builder.Property(o => o.ImpactedTaskId).IsRequired();
            builder.HasOne(o => o.PositionTask).WithMany(x => x.R5ImpactedTasks).HasForeignKey(y => y.PositionTaskId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.ImpactedTask).WithMany().HasForeignKey(y => y.ImpactedTaskId).OnDelete(DeleteBehavior.NoAction).IsRequired();
        }
    }
}