using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class Position_HistoryMap : Common.CommonMap<Position_History>
    {
        public override void Configure(EntityTypeBuilder<Position_History> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ChangeNotes).IsRequired();
            builder.Property(o => o.ChangeEffectiveDate).IsRequired();
            builder.HasOne(o => o.Position).WithMany(x => x.Position_Histories).HasForeignKey(y => y.PositionId).IsRequired();
        }
    }
}
