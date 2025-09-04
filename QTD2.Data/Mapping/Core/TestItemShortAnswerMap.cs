using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TestItemShortAnswerMap : Common.CommonMap<TestItemShortAnswer>
    {
        public override void Configure(EntityTypeBuilder<TestItemShortAnswer> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.AcceptableResponses).IsRequired();
            builder.Property(o => o.Responses).IsRequired();
            builder.Property(o => o.IsCaseSensitive).IsRequired();

            builder.HasOne(o => o.TestItem).WithMany(o => o.TestItemShortAnswers).HasForeignKey(k => k.TestItemId).IsRequired();
        }
    }
}
