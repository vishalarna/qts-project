using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Authentication;

namespace QTD2.Data.Mapping.Authentication
{
    public class InstanceSettingMap : Common.CommonMap<InstanceSetting>
    {
        public override void Configure(EntityTypeBuilder<InstanceSetting> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.DatabaseName).IsRequired().HasMaxLength(50);
            builder.Property(o => o.DataBaseVersion).IsRequired().HasDefaultValue("1");

            builder.HasOne(o => o.Instance).WithOne(k => k.InstanceSetting).HasForeignKey<InstanceSetting>(o => o.InstanceId);
            builder.HasOne(o => o.DefaultIdentityProvider).WithMany(k => k.InstanceSettings).HasForeignKey(o => o.DefaultIdentityProviderId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
