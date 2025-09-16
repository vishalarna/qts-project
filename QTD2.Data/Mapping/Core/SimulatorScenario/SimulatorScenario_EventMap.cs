using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenario_EventMap : Common.CommonMap<SimulatorScenario_Event>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_Event> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SimulatorScenarioId).IsRequired();
            builder.Property(o => o.Order).IsRequired();
            builder.Property(o => o.Title).IsRequired();
            builder.Property(o => o.Description);;
            builder.HasOne(o => o.SimulatorScenario).WithMany(o => o.Events).HasForeignKey(f => f.SimulatorScenarioId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
