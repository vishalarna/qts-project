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
    public class TestItemTrueFalseMap : Common.CommonMap<TestItemTrueFalse>
    {
        public override void Configure(EntityTypeBuilder<TestItemTrueFalse> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.TestItem).WithMany(m => m.TestItemTrueFalses).HasForeignKey(k => k.TestItemId).IsRequired();
            builder.Property(o => o.IsCorrect).HasDefaultValue(false).IsRequired();
            builder.Property(o => o.Choices).IsRequired().HasMaxLength(500);
        }
    }
}