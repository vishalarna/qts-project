using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Authentication
{
    public class EventLogMap : Common.CommonMap<EventLog>
    {
        public override void Configure(EntityTypeBuilder<EventLog> builder)
        {
            base.Configure(builder);

            builder.HasOne(o => o.User).WithMany().HasForeignKey(k => k.UserId).IsRequired();
        }
    }
}
