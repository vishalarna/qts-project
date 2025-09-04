using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenario_EnablingObjectives_LinkMap_Old : Common.CommonMap<SimulatorScenario_EnablingObjectives_Link_Old>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_EnablingObjectives_Link_Old> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.EnablingObjective).WithMany(m => m.SimulatorScenario_EnablingObjectives_Links).HasForeignKey(k => k.EnablingObjectiveID).IsRequired();
            builder.HasOne(o => o.SimulatorScenario).WithMany(m => m.SimulatorScenario_EnablingObjectives_Links).HasForeignKey(k => k.SimulatorScenarioID).IsRequired();
            builder.HasIndex(i => new { i.EnablingObjectiveID, i.SimulatorScenarioID }).IsUnique();
        }
    }
}
