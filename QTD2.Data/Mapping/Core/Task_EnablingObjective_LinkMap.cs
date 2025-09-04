using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Task_EnablingObjective_LinkMap : Common.CommonMap<Task_EnablingObjective_Link>
    {
        public override void Configure(EntityTypeBuilder<Task_EnablingObjective_Link> builder)
        {
            base.Configure(builder);

            builder.HasOne(o => o.Task).WithMany(m => m.Task_EnablingObjective_Links).HasForeignKey(k => k.TaskId).IsRequired();
            builder.HasOne(o => o.EnablingObjective).WithMany(m => m.Task_EnablingObjective_Links).HasForeignKey(k => k.EnablingObjectiveId).IsRequired();

            builder.HasIndex(i => new { i.EnablingObjectiveId, i.TaskId }).IsUnique();
        }
    }
}
