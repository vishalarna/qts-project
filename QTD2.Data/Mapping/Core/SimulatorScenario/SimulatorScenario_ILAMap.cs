using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenario_ILAMap : Common.CommonMap<SimulatorScenario_ILA>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_ILA> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SimulatorScenarioID).IsRequired();
            builder.Property(o => o.ILAID).IsRequired();
            builder.HasOne(o => o.SimulatorScenario).WithMany(o => o.ILAs).HasForeignKey(f => f.SimulatorScenarioID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.ILA).WithMany(o => o.SimulatorScenario_ILAs).HasForeignKey(f => f.ILAID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
