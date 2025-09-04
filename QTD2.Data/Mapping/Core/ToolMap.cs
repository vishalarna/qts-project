using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ToolMap : Common.CommonMap<Tool>
    {
        public override void Configure(EntityTypeBuilder<Tool> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).HasMaxLength(250).IsRequired();
            builder.Property(o => o.Number).HasMaxLength(10).IsRequired();
            builder.Property(o => o.Hyperlink).HasMaxLength(100);
            builder.Property(o => o.EffectiveDate);
            builder.Property(o => o.Upload);
            builder.Property(o => o.Description);

            builder.HasOne(o => o.ToolCategory).WithMany(m => m.Tools).HasForeignKey(k => k.ToolCategoryId).IsRequired();
        }
    }
}
