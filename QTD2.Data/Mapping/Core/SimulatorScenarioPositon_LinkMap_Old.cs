using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenarioPositon_LinkMap_Old : Common.CommonMap<SimulatorScenarioPositon_Link_Old>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenarioPositon_Link_Old> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Position).WithMany(m => m.SimulatorScenarioPositon_Links).HasForeignKey(k => k.PositionID).IsRequired();
            builder.HasOne(o => o.SimulatorScenario).WithMany(m => m.SimulatorScenarioPositon_Links).HasForeignKey(k => k.SimulatorScenarioID).IsRequired();
            builder.HasIndex(i => new { i.PositionID, i.SimulatorScenarioID }).IsUnique();
        }
    }
}
