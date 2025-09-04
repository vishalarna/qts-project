using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_EnablingObjectiveMap : Common.CommonMap<Version_EnablingObjective>
    {
        public override void Configure(EntityTypeBuilder<Version_EnablingObjective> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.EnablingObjectiveId).IsRequired();
            builder.Property(o => o.VersionNumber).IsRequired().HasMaxLength(20);
            builder.Property(o => o.CategoryId).IsRequired();
            builder.Property(o => o.SubCategoryId).IsRequired();
            builder.Property(o => o.Number).IsRequired();
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.State).IsRequired();
            builder.Property(o => o.IsInUse).HasDefaultValue(false);
            
            builder.HasOne(o => o.EnablingObjective).WithMany(m => m.Version_EnablingObjectives).HasForeignKey(k => k.EnablingObjectiveId).IsRequired();
            
        }
    }
}
