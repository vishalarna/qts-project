using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenario_EventAndScriptMap : Common.CommonMap<SimulatorScenario_EventAndScript>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_EventAndScript> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SimulatorScenarioId).IsRequired();
            builder.Property(o => o.Order).IsRequired();
            builder.Property(o => o.Title).IsRequired();
            builder.Property(o => o.Description);
            builder.Property(o => o.InitiatorId);
            builder.Property(o => o.Time);
            builder.HasOne(o => o.SimulatorScenario).WithMany(o => o.EventsAndScritps).HasForeignKey(f => f.SimulatorScenarioId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Initiator).WithMany(r => r.SimulatorScenario_EventAndScripts).HasForeignKey(f => f.InitiatorId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
