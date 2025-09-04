using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ProviderLevelMap : Common.CommonMap<ProviderLevel>
    {
        public override void Configure(EntityTypeBuilder<ProviderLevel> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(500);
        }
    }
}
