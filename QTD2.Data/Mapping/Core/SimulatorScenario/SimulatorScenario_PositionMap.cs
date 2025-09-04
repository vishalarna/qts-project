using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenario_PositionMap : Common.CommonMap<SimulatorScenario_Position>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_Position> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SimulatorScenarioID).IsRequired();
            builder.Property(o => o.PositionID).IsRequired();
            builder.HasOne(o => o.SimulatorScenario).WithMany(r => r.Positions).HasForeignKey(f => f.SimulatorScenarioID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Position).WithMany(o => o.SimulatorScenarioPositions).HasForeignKey(f => f.PositionID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
