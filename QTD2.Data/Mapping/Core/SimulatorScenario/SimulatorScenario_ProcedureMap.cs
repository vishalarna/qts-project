using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenario_ProcedureMap : Common.CommonMap<SimulatorScenario_Procedure>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_Procedure> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SimulatorScenarioId).IsRequired();
            builder.Property(o => o.ProcedureId).IsRequired();
            builder.HasOne(o => o.SimulatorScenario).WithMany(r => r.Procedures).HasForeignKey(f => f.SimulatorScenarioId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Procedure).WithMany(o => o.SimulatorScenario_Procedures).HasForeignKey(f => f.ProcedureId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
