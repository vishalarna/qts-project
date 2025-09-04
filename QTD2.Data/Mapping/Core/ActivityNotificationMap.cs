using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ActivityNotificationMap : Common.CommonMap<ActivityNotification>
    {
        public override void Configure(EntityTypeBuilder<ActivityNotification> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Title).IsRequired().HasMaxLength(200);
        }
    }
}
