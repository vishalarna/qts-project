using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Procedure_EnablingObjective_LinkMap : Common.CommonMap<Procedure_EnablingObjective_Link>
    {
        public override void Configure(EntityTypeBuilder<Procedure_EnablingObjective_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.EnablingObjective).WithMany(m => m.Procedure_EnablingObjective_Links).HasForeignKey(k => k.EnablingObjectiveId).IsRequired();
            builder.HasOne(o => o.Procedure).WithMany(m => m.Procedure_EnablingObjective_Links).HasForeignKey(k => k.ProcedureId).IsRequired();

            builder.HasIndex(i => new { i.EnablingObjectiveId, i.ProcedureId }).IsUnique();
        }
    }
}
