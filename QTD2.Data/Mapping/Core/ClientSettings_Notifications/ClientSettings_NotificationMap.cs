using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClientSettings_NotificationMap : Common.CommonMap<ClientSettings_Notification>
    {
        public override void Configure(EntityTypeBuilder<ClientSettings_Notification> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.TimingText);
            builder.Property(o => o.Enabled).IsRequired();

            builder.HasMany(r => r.Steps).WithOne(r => r.ClientSettings_Notification).HasForeignKey("ClientSettingsNotificationId");
            builder.HasMany(r => r.AvailableCustomSettings).WithOne().HasForeignKey("ClientSettingsNotificationId");
            builder.HasMany(r => r.CustomSettings).WithOne().HasForeignKey("ClientSettingsNotificationId");
        }
    }
}
