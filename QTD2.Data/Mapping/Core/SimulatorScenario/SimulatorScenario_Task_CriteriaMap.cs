using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenario_Task_CriteriaMap : Common.CommonMap<SimulatorScenario_Task_Criteria>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_Task_Criteria> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SimulatorScenarioId).IsRequired();
            builder.Property(o => o.TaskId).IsRequired();
            builder.Property(o => o.Criteria);
            builder.HasOne(o => o.SimulatorScenario).WithMany(o=>o.TaskCriterias).HasForeignKey(f => f.SimulatorScenarioId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Task).WithMany(o=>o.SimulatorScenario_Task_Criterias).HasForeignKey(f => f.TaskId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
