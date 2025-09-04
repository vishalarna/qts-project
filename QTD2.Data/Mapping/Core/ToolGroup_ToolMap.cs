using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ToolGroup_ToolMap : Common.CommonMap<ToolGroup_Tool>
    {
        public override void Configure(EntityTypeBuilder<ToolGroup_Tool> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ToolGroup).WithMany(m => m.ToolGroup_Tools).HasForeignKey(k => k.ToolGroupId).IsRequired();
            builder.HasOne(o => o.Tool).WithMany(m => m.ToolGroup_Tools).HasForeignKey(k => k.ToolId).IsRequired();

            builder.HasIndex(i => new { i.ToolId, i.ToolGroupId }).IsUnique();
        }
    }
}
