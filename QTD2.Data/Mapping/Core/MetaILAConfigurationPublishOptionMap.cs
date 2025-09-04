using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;


namespace QTD2.Data.Mapping.Core
{
    public class MetaILAConfigurationPublishOptionMap : Common.CommonMap<MetaILAConfigurationPublishOption>
    {
        public override void Configure(EntityTypeBuilder<MetaILAConfigurationPublishOption> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description).HasMaxLength(50).IsRequired();
        }
    }
}
