using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class CertifyingBodyMap : Common.CommonMap<CertifyingBody>
    {
        public override void Configure(EntityTypeBuilder<CertifyingBody> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.EnableCertifyingBodyLevelCEHEditing).HasDefaultValue(false);
            builder.HasIndex(o => new { o.Name }).IsUnique();
        }
    }
}
