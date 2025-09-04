using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClientSettings_LabelReplacementsMap : Common.CommonMap<ClientSettings_LabelReplacement>
    {
        public override void Configure(EntityTypeBuilder<ClientSettings_LabelReplacement> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.DefaultLabel).IsRequired().HasMaxLength(200);
            builder.Property(o => o.LabelReplacement).HasMaxLength(200);
        }
    }
}
