using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
 
    public class Position_TaskMap : Common.CommonMap<Position_Task>
    {
        public override void Configure(EntityTypeBuilder<Position_Task> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Position).WithMany(x => x.Position_Tasks).HasForeignKey(y => y.PositionId).IsRequired();
            builder.HasOne(o => o.Task).WithMany(x => x.Position_Tasks).HasForeignKey(y => y.TaskId).IsRequired();
        }
    }
}
