using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QTD2.Data.Mapping.Core
{
    public class TaskMap : Common.CommonMap<Domain.Entities.Core.Task>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Core.Task> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.Number);
            builder.Property(o => o.Image);
            builder.Property(o => o.Abreviation);
            builder.Property(o => o.Criteria);
            builder.Property(o => o.TaskCriteriaUpload);
            builder.Property(o => o.Conditions);
            builder.Property(o => o.Critical).HasDefaultValue(false).IsRequired();
            builder.Property(o => o.IsMeta).HasDefaultValue(false).IsRequired();
            builder.Property(o => o.IsReliability).HasDefaultValue(false).IsRequired();
            builder.Property(o => o.EffectiveDate).IsRequired();
            builder.HasOne(o => o.SubdutyArea).WithMany(m => m.Tasks).HasForeignKey(o => o.SubdutyAreaId).IsRequired();
            builder.HasIndex(o => new { o.SubdutyAreaId, o.Number }).IsUnique().HasFilter("[Deleted] = 0");

            builder.Navigation(n => n.SubdutyArea);
        }
    }
}
