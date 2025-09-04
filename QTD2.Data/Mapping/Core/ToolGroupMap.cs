using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ToolGroupMap : Common.CommonMap<ToolGroup>
    {
        public override void Configure(EntityTypeBuilder<ToolGroup> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description).HasMaxLength(250).IsRequired();
        }
    }
}
