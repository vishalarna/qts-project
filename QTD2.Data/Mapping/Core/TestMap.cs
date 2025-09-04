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
    public class TestMap : Common.CommonMap<Test>
    {
        public override void Configure(EntityTypeBuilder<Test> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.IsPublished).HasDefaultValue(false);
            builder.Property(o => o.EffectiveDate);
            builder.Property(o => o.RandomizeDistractors).HasDefaultValue(false);
            builder.HasOne(o => o.TestStatus).WithMany(m => m.Tests).HasForeignKey(k => k.TestStatusId).IsRequired();
            builder.Property(o => o.TestTitle).IsRequired().HasMaxLength(200);
            builder.Property(o => o.RandomizeQuestionsSequence).HasDefaultValue(false);
        }
    }
}