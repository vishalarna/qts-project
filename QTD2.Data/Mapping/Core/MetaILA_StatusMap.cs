using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class MetaILA_StatusMap : Common.CommonMap<MetaILA_Status>
    {
        public override void Configure(EntityTypeBuilder<MetaILA_Status> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(50);
        }
    }
}
