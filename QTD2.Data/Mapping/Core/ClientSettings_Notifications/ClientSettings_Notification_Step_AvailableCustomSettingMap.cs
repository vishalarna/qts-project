using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ClientSettings_Notification_Step_AvailableCustomSettingMap : Common.CommonMap<ClientSettings_Notification_Step_AvailableCustomSetting>
    {
        public override void Configure(EntityTypeBuilder<ClientSettings_Notification_Step_AvailableCustomSetting> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.Setting).IsRequired().HasMaxLength(200);
            builder.Property(o => o.ClientSettingsNotificationStepId).IsRequired();
        }
    }
}
