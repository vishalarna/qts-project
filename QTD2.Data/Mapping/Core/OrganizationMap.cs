using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class OrganizationMap : Common.CommonMap<Organization>
    {
        public override void Configure(EntityTypeBuilder<Organization> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(200);
        }
    }
}
