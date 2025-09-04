using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ProcedureMap : Common.CommonMap<Procedure>
    {
        public override void Configure(EntityTypeBuilder<Procedure> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description);
            builder.Property(o => o.Title).HasMaxLength(5000).IsRequired();
            builder.Property(o => o.Number).HasMaxLength(50).IsRequired();
            builder.Property(o => o.EffectiveDate).IsRequired();
            //builder.Property(o => o.IsActive).IsRequired();
            builder.Property(o => o.IsDeleted).IsRequired();
            builder.Property(o => o.IsPublished).IsRequired();
            builder.Property(o => o.FileName);

            builder.HasOne(o => o.Procedure_IssuingAuthority).WithMany(m => m.Procedures).HasForeignKey(k => k.IssuingAuthorityId).IsRequired();

            //builder.HasIndex(i => new { i.IssuingAuthorityId, i.Number }).IsUnique().HasFilter("[Number] IS NOT NULL");
        }
    }
}
