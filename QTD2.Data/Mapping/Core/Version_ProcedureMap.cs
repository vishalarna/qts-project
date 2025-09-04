using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_ProcedureMap : Common.CommonMap<Version_Procedure>
    {
        public override void Configure(EntityTypeBuilder<Version_Procedure> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ProcedureNumber).IsRequired();
            builder.Property(o => o.Title).IsRequired();
            builder.Property(o => o.MajorVersion).IsRequired();
            builder.Property(o => o.MinorVersion).IsRequired();
            builder.Property(o => o.VersionNumber).HasMaxLength(20);
            builder.HasOne(o => o.Procedure).WithMany(m => m.Version_Procedures).HasForeignKey(k => k.ProcedureId).IsRequired();
        }
    }
}
