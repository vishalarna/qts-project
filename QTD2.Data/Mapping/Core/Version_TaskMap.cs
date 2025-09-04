using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_TaskMap : Common.CommonMap<Version_Task>
    {
        public override void Configure(EntityTypeBuilder<Version_Task> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.TaskNumber);
            builder.Property(o => o.Description);
            builder.Property(o => o.Conditions);
            builder.Property(o => o.VersionNumber);
            builder.Property(o => o.Standards);
            builder.Property(o => o.Critical);
            builder.Property(o => o.References);
            builder.Property(o => o.RequiredTime);
            builder.Property(o => o.IsInUse).HasDefaultValue(false);
            builder.Property(o => o.State).HasDefaultValue(0);
            //builder.Property(o => o.MajorVersion).IsRequired();
            //builder.Property(o => o.MinorVersion).IsRequired();
            builder.Property(o => o.TaskActive);
            builder.HasOne(o => o.Task).WithMany(m => m.Version_Tasks).HasForeignKey(k => k.TaskId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
