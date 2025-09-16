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
    public class SimulatorScenario_ScriptMap : Common.CommonMap<SimulatorScenario_Script>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_Script> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.EventId).IsRequired();
            builder.Property(o => o.Title).IsRequired();
            builder.Property(o => o.Description);
            builder.Property(o => o.InitiatorId);
            builder.Property(o => o.Time);
            builder.HasOne(o => o.Initiator).WithMany(r => r.SimulatorScenario_Scripts).HasForeignKey(f => f.InitiatorId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.SimulatorScenario_Event).WithMany(r => r.Scripts).HasForeignKey(f => f.EventId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
