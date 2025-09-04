using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenario_DifficultyMap : Common.CommonMap<SimulatorScenario_Difficulty>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_Difficulty> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Difficulty).IsRequired();
        }
    }
}
