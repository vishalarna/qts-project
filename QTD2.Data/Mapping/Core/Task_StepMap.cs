using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Task_StepMap : Common.CommonMap<Task_Step>
    {
        public override void Configure(EntityTypeBuilder<Task_Step> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.Number);
            builder.Property(o => o.ParentStepId);
            builder.HasOne(o => o.Task).WithMany(m => m.Task_Steps).HasForeignKey(k => k.TaskId).IsRequired();

            //builder.HasIndex(i => new { i.TaskId, i.Number }).IsUnique().HasFilter("[Number] IS NOT NULL");
        }
    }
}
