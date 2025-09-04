using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class QTDUserMap : Common.CommonMap<QTDUser>
    {
        public override void Configure(EntityTypeBuilder<QTDUser> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Person).WithOne(o => o.QTDUser).HasForeignKey<QTDUser>(k => k.PersonId).IsRequired();
        }
    }
}
