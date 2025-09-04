using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenarioDifficultyLevelMap_Old : Common.CommonMap<SimulatorScenarioDifficultyLevel_Old>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenarioDifficultyLevel_Old> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SimulatorScenarioDiffLevel).HasMaxLength(50).IsRequired();
        }
    }
}
