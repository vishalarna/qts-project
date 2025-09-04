using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_ToolMap : Common.CommonMap<Version_Tool>
    {
        public override void Configure(EntityTypeBuilder<Version_Tool> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.MinorVersion).IsRequired();
            builder.Property(o => o.MajorVersion).IsRequired();

            builder.HasOne(o => o.Tool).WithMany(m => m.Version_Tools).HasForeignKey(k => k.ToolId).IsRequired();
        }
    }
}
