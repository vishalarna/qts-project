using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Procedure_IssuingAuthorityMap : Common.CommonMap<Procedure_IssuingAuthority>
    {
        public override void Configure(EntityTypeBuilder<Procedure_IssuingAuthority> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description);
            builder.Property(o => o.Title).HasMaxLength(2000).IsRequired();
            builder.Property(o => o.Website).HasMaxLength(200).IsRequired();
            builder.Property(o => o.EffectiveDate).IsRequired();
            builder.Property(o => o.IsActive).HasDefaultValue(true).IsRequired();
            builder.Property(o => o.IsDeleted).HasDefaultValue(false).IsRequired();
        }
    }
}
