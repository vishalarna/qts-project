using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TestItemMatchMap : Common.CommonMap<TestItemMatch>
    {
        public override void Configure(EntityTypeBuilder<TestItemMatch> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ChoiceDescription).HasMaxLength(500);
            builder.Property(o => o.MatchDescription).HasMaxLength(500).IsRequired();
            builder.Property(o => o.MatchValue).HasMaxLength(1).IsRequired();
            builder.Property(o => o.CorrectValue).HasMaxLength(1);

            builder.HasOne(o => o.TestItem).WithMany(o => o.TestItemMatches).HasForeignKey(k => k.TestItemId).IsRequired();
        }
    }
}
