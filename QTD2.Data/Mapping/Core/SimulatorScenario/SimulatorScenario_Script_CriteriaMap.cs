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
    public class SimulatorScenario_Script_CriteriaMap : Common.CommonMap<SimulatorScenario_Script_Criteria>
    {
        public override void Configure(EntityTypeBuilder<SimulatorScenario_Script_Criteria> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.CriteriaId).IsRequired();
            builder.Property(o => o.ScriptId);
            builder.HasOne(o => o.Script).WithMany(o => o.Criterias).HasForeignKey(f => f.ScriptId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Criteria).WithMany().HasForeignKey(f => f.CriteriaId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
