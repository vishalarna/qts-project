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
    public class EnablingObjectiveHistoryMap : Common.CommonMap<EnablingObjectiveHistory>
    {
        public override void Configure(EntityTypeBuilder<EnablingObjectiveHistory> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.OldStatus).IsRequired();
            builder.Property(o => o.NewStatus).IsRequired();
            builder.Property(o => o.ChangeEffectiveDate).IsRequired();
            builder.Property(o => o.ChangeNotes);
            builder.HasOne(o => o.EnablingObjective).WithMany(x => x.EnablingObjectiveHistories).HasForeignKey(y => y.EnablingObjectiveId).IsRequired();

            builder.HasOne(o => o.Version_EnablingObjective).WithMany(x => x.EnablingObjectiveHistories).HasForeignKey(y => y.Version_EnablingObjectiveId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}
