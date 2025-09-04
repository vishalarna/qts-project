using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Task_PositionMap : Common.CommonMap<Task_Position>
    {
        public override void Configure(EntityTypeBuilder<Task_Position> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Task).WithMany(m => m.Task_Positions).HasForeignKey(k => k.TaskId).IsRequired();
            builder.HasOne(o => o.Position).WithMany(m => m.Task_Positions).HasForeignKey(k => k.PositionId).IsRequired();
        }
    }
}
