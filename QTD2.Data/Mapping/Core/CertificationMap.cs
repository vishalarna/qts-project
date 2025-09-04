using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class CertificationMap : Common.CommonMap<Certification>
    {
        public override void Configure(EntityTypeBuilder<Certification> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(200);
            builder.Property(o => o.InternalIdentifier);
            builder.HasOne(o => o.CertifyingBody).WithMany(m => m.Certifications).HasForeignKey(o => o.CertifyingBodyId).IsRequired();
        }
    }
}
