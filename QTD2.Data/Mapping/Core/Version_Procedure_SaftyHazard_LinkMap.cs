using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    internal class Version_Procedure_SaftyHazard_LinkMap : Common.CommonMap<Version_Procedure_SaftyHazard_Link>
    {
        public override void Configure(EntityTypeBuilder<Version_Procedure_SaftyHazard_Link> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.VersionNumber).HasMaxLength(20);
            builder.HasOne(o => o.Version_Procedure).WithMany(m => m.Version_Procedure_SaftyHazard_Links).HasForeignKey(k => k.Version_ProcedureId).IsRequired();
            builder.HasOne(o => o.Version_SaftyHazard).WithMany(m => m.Version_Procedure_SaftyHazard_Links).HasForeignKey(k => k.Version_SaftyHazardId).IsRequired();
        }
    }
}
