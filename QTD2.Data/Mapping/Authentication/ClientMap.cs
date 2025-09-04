using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Data.Mapping.Common;
using QTD2.Domain.Entities.Authentication;

namespace QTD2.Data.Mapping.Authentication
{
    public class ClientMap : CommonMap<Client>
    {
        public override void Configure(EntityTypeBuilder<Client> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(200);
        }
    }
}
