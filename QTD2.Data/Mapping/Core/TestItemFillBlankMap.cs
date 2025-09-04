using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TestItemFillBlankMap : Common.CommonMap<TestItemFillBlank>
    {
        public override void Configure(EntityTypeBuilder<TestItemFillBlank> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.TestItem).WithMany(m => m.TestItemFillBlanks).HasForeignKey(k => k.TestItemId).IsRequired();
            builder.Property(o => o.CorrectIndex).IsRequired();
            builder.Property(o => o.Correct).IsRequired().HasMaxLength(500);
        }
    }
}