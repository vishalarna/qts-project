using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Test_HistoryMap : Common.CommonMap<Test_History>
    {
        public override void Configure(EntityTypeBuilder<Test_History> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.ChangeNotes);
            builder.Property(x => x.EffectiveDate);
            builder.HasOne(k => k.Test).WithMany(o => o.Test_Histories).HasForeignKey(x => x.TestId).IsRequired();
        }
    }
}
