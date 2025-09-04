using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ILA_ResourceMap : Common.CommonMap<ILA_Resource>
    {
        public override void Configure(EntityTypeBuilder<ILA_Resource> builder)
        {
            base.Configure(builder);

            builder.HasOne(o => o.ILA).WithMany(m => m.ILA_Resources).HasForeignKey(k => k.ILAId).IsRequired();

            builder.Property(o => o.ResourceNumber);
            builder.Property(o => o.Title);
            builder.Property(o => o.Section);
            builder.Property(o => o.Chapter);
            builder.Property(o => o.Hyperlink);
            builder.Property(o => o.HyperlinkText);
            builder.Property(o => o.Comments);
        }
    }
}
