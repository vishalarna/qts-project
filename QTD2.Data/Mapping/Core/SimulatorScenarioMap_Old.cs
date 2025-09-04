using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenarioMap_Old : Common.CommonMap<SimulatorScenario_Old>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_Old> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SimScenarioTitle).IsRequired().HasMaxLength(200);
            builder.Property(o => o.SimScenarioDesc).IsRequired().HasMaxLength(500);
            builder.Property(o => o.SimScenarioDurationHours).HasDefaultValue(0).IsRequired();
            builder.Property(o => o.SimScenarioDurationMins).HasDefaultValue(0).IsRequired();
            builder.HasOne(o => o.SimulatorScenarioDifficultyLevel).WithMany(m => m.SimulatorScenarios).HasForeignKey(k => k.SimScenarioDiffID).IsRequired();        }
    }
}