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
    public class TestItem_HistoryMap : Common.CommonMap<TestItem_History>
    {
        public override void Configure(EntityTypeBuilder<TestItem_History> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.OldStatus).HasDefaultValue(false);
            builder.Property(o => o.NewStatus).HasDefaultValue(true);
            builder.Property(o => o.EffectiveDate);
            builder.Property(o => o.ChangeNotes).HasMaxLength(200);
            builder.HasOne(o => o.TestItem).WithMany(x => x.TestItem_Histories).HasForeignKey(y => y.TestItemId).IsRequired();
        }
    }
}
