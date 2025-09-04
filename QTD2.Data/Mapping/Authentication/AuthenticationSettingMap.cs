using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Data.Mapping.Common;
using QTD2.Domain.Entities.Authentication;

namespace QTD2.Data.Mapping.Authentication
{
    public class AuthenticationSettingMap : CommonMap<AuthenticationSetting>
    {
        public override void Configure(EntityTypeBuilder<AuthenticationSetting> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.VersionMajor).IsRequired();
            builder.Property(o => o.VersionMinor).IsRequired();
            builder.Property(o => o.VersionPatch).IsRequired();
        }
    }
}
