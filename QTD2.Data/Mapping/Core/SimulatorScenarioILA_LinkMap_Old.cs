using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenarioILA_LinkMap_Old : Common.CommonMap<SimulatorScenarioILA_Link_Old>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenarioILA_Link_Old> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.SimulatorScenarioILA_Links).HasForeignKey(k => k.ILAID).IsRequired();
            builder.HasOne(o => o.SimulatorScenario).WithMany(m => m.SimulatorScenarioILA_Links).HasForeignKey(k => k.SimulatorScenarioID).IsRequired();
            builder.HasIndex(i => new { i.ILAID, i.SimulatorScenarioID }).IsUnique();
        }
    }
}
