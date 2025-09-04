using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenario_PrerequisiteMap : Common.CommonMap<SimulatorScenario_Prerequisite>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_Prerequisite> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SimulatorScenarioId).IsRequired();
            builder.Property(o => o.PrerequisiteId).IsRequired();
            builder.HasOne(o => o.SimulatorScenario).WithMany(o=>o.Prerequisites).HasForeignKey(f => f.SimulatorScenarioId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Prerequisite).WithMany(o=>o.SimulatorScenario_Prerequisites).HasForeignKey(f => f.PrerequisiteId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
