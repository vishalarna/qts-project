using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenario_StatusMap : Common.CommonMap<SimulatorScenario_Status>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_Status> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Status).IsRequired();
        }
    }
}