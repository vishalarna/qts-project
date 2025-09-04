using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenario_TaskMap : Common.CommonMap<SimulatorScenario_Task>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_Task> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SimulatorScenarioId).IsRequired();
            builder.Property(o => o.TaskId).IsRequired();
            builder.HasOne(o => o.SimulatorScenario).WithMany(r => r.Tasks).HasForeignKey(f => f.SimulatorScenarioId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Task).WithMany(o => o.SimulatorScenario_Tasks).HasForeignKey(f => f.TaskId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
