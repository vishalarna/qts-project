using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class EmpSettingsReleaseTypeMap : Common.CommonMap<EmpSettingsReleaseType>
    {
        public override void Configure(EntityTypeBuilder<EmpSettingsReleaseType> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Type).IsRequired();
        }
    }
}