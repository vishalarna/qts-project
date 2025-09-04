using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Task_ToolMap : Common.CommonMap<Task_Tool>
    {
        public override void Configure(EntityTypeBuilder<Task_Tool> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Task).WithMany(m => m.Task_Tools).HasForeignKey(k => k.TaskId).IsRequired();
            builder.HasOne(o => o.Tool).WithMany(m => m.Task_Tools).HasForeignKey(k => k.ToolId).IsRequired();
            builder.HasIndex(i => new { i.TaskId, i.ToolId }).IsUnique().HasFilter("[Deleted] = 0");
        }
    }
}
