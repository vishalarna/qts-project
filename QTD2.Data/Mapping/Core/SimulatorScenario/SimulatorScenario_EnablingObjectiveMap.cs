using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
   public class SimulatorScenario_EnablingObjectiveMap : Common.CommonMap<SimulatorScenario_EnablingObjective>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_EnablingObjective> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SimulatorScenarioID).IsRequired();
            builder.Property(o => o.EnablingObjectiveID).IsRequired();
            builder.HasOne(o => o.SimulatorScenario).WithMany(r => r.EnablingObjectives).HasForeignKey(f => f.SimulatorScenarioID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.EnablingObjective).WithMany(o => o.SimulatorScenario_EnablingObjectives).HasForeignKey(f => f.EnablingObjectiveID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
