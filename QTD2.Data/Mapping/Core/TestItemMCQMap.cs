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
    public class TestItemMCQMap : Common.CommonMap<TestItemMCQ>
    {
        public override void Configure(EntityTypeBuilder<TestItemMCQ> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ChoiceDescription).IsRequired();
            builder.Property(o => o.IsCorrect).HasDefaultValue(false).IsRequired();
            builder.HasOne(o => o.TestItem).WithMany(o => o.TestItemMCQs).HasForeignKey(k => k.TestItemId).IsRequired();
        }
    }
}
