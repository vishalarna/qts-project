using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenarioTaskObjectives_LinkMap_Old : Common.CommonMap<SimulatorScenarioTaskObjectives_Link_Old>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenarioTaskObjectives_Link_Old> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Task).WithMany(m => m.SimulatorScenarioTaskObjectives_Links).HasForeignKey(k => k.TaskID).IsRequired();
            builder.HasOne(o => o.SimulatorScenario).WithMany(m => m.SimulatorScenarioTaskObjectives_Links).HasForeignKey(k => k.SimulatorScenarioID).IsRequired();
            builder.HasIndex(i => new { i.TaskID, i.SimulatorScenarioID }).IsUnique();
        }
    }
}
