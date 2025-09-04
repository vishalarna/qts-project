using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class SimulatorScenario_EventAndScript_CriteriaMap : Common.CommonMap<SimulatorScenario_EventAndScript_Criteria>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_EventAndScript_Criteria> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.EventAndScriptId).IsRequired();
            builder.Property(o => o.CriteriaId).IsRequired();
            builder.HasOne(o => o.EventAndScript).WithMany(o=>o.Criterias).HasForeignKey(f => f.EventAndScriptId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Criteria).WithMany().HasForeignKey(f => f.CriteriaId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
