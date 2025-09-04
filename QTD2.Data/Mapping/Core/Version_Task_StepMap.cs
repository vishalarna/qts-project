using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_Task_StepMap : Common.CommonMap<Version_Task_Step>
    {
        public override void Configure(EntityTypeBuilder<Version_Task_Step> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.Number).IsRequired();

            builder.HasOne(o => o.Version_Task).WithMany(m => m.Version_Task_Steps).HasForeignKey(k => k.VersionTaskId).IsRequired();
            builder.HasOne(o => o.Task_Step).WithMany(m => m.Version_Task_Steps).HasForeignKey(k => k.TaskStepId).IsRequired();
        }
    }
}
