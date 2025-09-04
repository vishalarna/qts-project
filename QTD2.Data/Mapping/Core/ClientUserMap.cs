using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClientUserMap : Common.CommonMap<ClientUser>
    {
        public override void Configure(EntityTypeBuilder<ClientUser> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Person).WithOne(o => o.ClientUser).HasForeignKey<ClientUser>(k => k.PersonId).IsRequired();
        }
    }
}
