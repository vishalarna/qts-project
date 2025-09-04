using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Procedure_SaftyHazard_LinkMap : Common.CommonMap<Procedure_SaftyHazard_Link>
    {
        public override void Configure(EntityTypeBuilder<Procedure_SaftyHazard_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.SaftyHazard).WithMany(m => m.Procedure_SaftyHazard_Links).HasForeignKey(k => k.SaftyHazardId).IsRequired();
            builder.HasOne(o => o.Procedure).WithMany(m => m.Procedure_SaftyHazard_Links).HasForeignKey(k => k.ProcedureId).IsRequired();

            builder.HasIndex(i => new { i.SaftyHazardId, i.ProcedureId }).IsUnique();
        }
    }
}
